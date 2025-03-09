using Castle.Core.Internal;
using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.GHN.Request;
using CleanArchitecture.Application.DTOs.GHN.Response;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.DTOs.OrderDto;
using CleanArchitecture.Application.DTOs.OrderItemDto;
using CleanArchitecture.Application.DTOs.UserDto;
using CleanArchitecture.Application.DTOs.VnPay;
using CleanArchitecture.Application.Enums;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

public class OrderService : IOrderService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IClaimsService _claimsService;
  private readonly IGHNService _ghnService;
  private readonly IEnumerable<IInvoiceGenerateStrategy> _invoiceGenerateStrategies;
  private readonly ITimeZoneService _timeZoneService;
  private readonly IVnPayIntegrationService _vnPayIntegrationService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IValidator<CreateOnlineOrderRequest> _createOrderRequestValidator;
  private readonly IValidator<CreateWalkInOrderRequest> _createWalkInOrderRequestValidator;
  private readonly UserManager<User> _userManager;

  // Exchange rate from USD to VND - should ideally come from a service
  private const decimal USD_TO_VND_RATE = 24850;

  public OrderService(
    IUnitOfWork unitOfWork,
    IErrorFactory errorFactory,
    IClaimsService claimsService,
    IGHNService ghnService,
    ITimeZoneService timeZoneService,
    IVnPayIntegrationService vnPayIntegrationService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateOnlineOrderRequest> createOrderRequestValidator,
    UserManager<User> userManager, IValidator<CreateWalkInOrderRequest> createWalkInOrderRequestValidator, IEnumerable<IInvoiceGenerateStrategy> invoiceGenerateStrategies)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _claimsService = claimsService;
    _ghnService = ghnService;
    _timeZoneService = timeZoneService;
    _vnPayIntegrationService = vnPayIntegrationService;
    _httpContextAccessor = httpContextAccessor;
    _createOrderRequestValidator = createOrderRequestValidator;
    _userManager = userManager;
    _createWalkInOrderRequestValidator = createWalkInOrderRequestValidator;
    _invoiceGenerateStrategies = invoiceGenerateStrategies;
  }

  // 1. Initiate Order (First step of checkout)
  public async Task<Result<OrderResponse>> InitiateOrder(CreateOnlineOrderRequest request)
  {
    try
    {
      var validationResult = await _createOrderRequestValidator.ValidateAsync(request);
      if (!validationResult.IsValid)
      {
        var errors = validationResult.Errors
          .Select(e => new Error("ValidationError", e.ErrorMessage))
          .ToList();

        return Result<OrderResponse>.Failure(errors, StatusCodes.Status400BadRequest);
      }

      // Validate cart and user
      var cartValidationResult = await ValidateOrderRequest(request);
      if (!cartValidationResult.IsSuccess)
      {
        // Create a proper Result<OrderResponse> from the Result<Cart>
        return Result<OrderResponse>.Failure(
          cartValidationResult.Errors,
          cartValidationResult.Status);
      }

      var cart = cartValidationResult.Data;

      // Check and deduct inventory
      var inventoryResult = await CheckAndDeductInventory(cart);
      if (!inventoryResult.IsSuccess)
      {
        return Result<OrderResponse>.Failure(
          inventoryResult.Errors,
          inventoryResult.Status);
      }

      // Calculate shipping dimensions
      var shippingDetails = CalculateShippingDetails(cart.CartItems);

      // Convert currency if needed
      decimal totalPrice = cart.TotalPrice;
      if (request.Currency == "USD")
      {
        totalPrice = ConvertUsdToVnd(totalPrice);
      }

      var customer = await _userManager.FindByIdAsync(_claimsService.CurrentUserId.ToString());

      // Create order
      var order = CreateOrderEntity(request, cart);
      decimal subTotal = 0;
      foreach (var item in order.OrderItems)
      {
        subTotal += await _unitOfWork.Cosmetics.GetCosmeticPrice(item.Cosmetic) * item.Quantity;
        item.SellingPrice = await _unitOfWork.Cosmetics.GetCosmeticPrice(item.Cosmetic);
      }

      order.SubTotal = subTotal;

      // Get Shop Info for Create a Shipping Orer
      var shopInfo = await _ghnService.GetStoreInformationAsync();

      decimal codAmount = request.PaymentMethod == PaymentMethods.COD ? totalPrice : 0;
      int paymentTypeId = request.PaymentMethod == PaymentMethods.COD ? 2 : 1;

      // Create Shipping Order in GHN
      var ghnOrderResult = await _ghnService.CreateShippingOrderAsync(MapToCreateShippingOrderRequest(order,
        shopInfo.Data!, request, shippingDetails, customer!, codAmount, paymentTypeId));
      if (!ghnOrderResult.IsSuccess)
      {
        return Result<OrderResponse>.Failure(ghnOrderResult.Errors, ghnOrderResult.Status);
      }

      // Save GHN Order ID & Tracking Number, also increase the total price
      order.TrackingNumber = ghnOrderResult.Data!.OrderCode;
      order.TotalPrice = ghnOrderResult.Data.TotalFee + order.SubTotal;

      // Generate order response
      var orderResponse = MapToOrderResponse(order);

      // Handle payment method specific logic
      await HandlePaymentMethod(request.PaymentMethod, order, orderResponse);

      // Save order to database
      _unitOfWork.Orders.Create(order);

      // Clear customer cart after creating the
      _unitOfWork.Carts.Remove(cart);
      var saved = await _unitOfWork.CompleteAsync();

      if (!saved)
      {
        return Result<OrderResponse>.Failure(
          [new Error("Order.Create", "Failed to save order")],
          StatusCodes.Status500InternalServerError);
      }

      var invoiceStrategy = _invoiceGenerateStrategies.OfType<OnlineInvoiceStrategy>().FirstOrDefault();
      byte[] invoice = [];
      if (invoiceStrategy != null)
      {
        invoice = await invoiceStrategy.GenerateAsync(order, _unitOfWork);
      }

      orderResponse.Invoice = invoice;

      return Result<OrderResponse>.Success(
        orderResponse,
        StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<OrderResponse>.Failure(
        [new Error("Order.Create", ex.Message)],
        StatusCodes.Status500InternalServerError);
    }
  }

  public async Task<Result<OrderResponse>> InitiateOrder(CreateWalkInOrderRequest request)
  {
    var validationResult = await _createWalkInOrderRequestValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var error = _errorFactory.CreateValidationError(nameof(request), validationResult);
      return Result<OrderResponse>.Failure(error.errs, error.statusCode);
    }

    if (request.CouponId is not null)
    {
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.CouponId);
      if (coupon is null)
      {
        var error = _errorFactory.CreateInvalidCouponError();
        return Result<OrderResponse>.Failure([error.err], error.statusCode);
      }
    }

    var customer = await _unitOfWork.Users.GetByPhoneNumberAsync(request.CustomerPhoneNumber!);

    if (customer is null)
    {
      customer = new User
      {
        Id = Guid.NewGuid(),
        FirstName = request.FirstName,
        LastName = request.LastName,
        UserName = request.CustomerPhoneNumber,
        PhoneNumber = request.CustomerPhoneNumber
      };

      var result = await _userManager.CreateAsync(customer, request.CustomerPhoneNumber!);
      if (!result.Succeeded)
      {
        var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
        return Result<OrderResponse>.Failure(errors, StatusCodes.Status500InternalServerError);
      }
      var roleResult = await _userManager.AddToRolesAsync(customer, [Roles.Customer]);
      if (!roleResult.Succeeded)
      {
        var errors = roleResult.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
        return Result<OrderResponse>.Failure(errors, StatusCodes.Status500InternalServerError);
      }
    }

    var order = new Order()
    {
      CustomerId = customer.Id,
      CouponId = request.CouponId,
      CreateAt = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      OrderDate = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      IsActive = true
    };

    List<OrderItem> orderItems = new List<OrderItem>();
    decimal subTotal = 0;
    decimal totalPrice = 0;

    foreach (var orderItem in request.Cosmetics)
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(orderItem.Key);
      if (cosmetic is null)
        return null;

      var originalPrice = await _unitOfWork.Cosmetics.GetCosmeticOriginalPrice(cosmetic);
      var sellingPrice = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetic);
      var quantity = orderItem.Value;
      subTotal += originalPrice * quantity;
      totalPrice += sellingPrice * quantity;

      orderItems.Add(new OrderItem()
      {
        CosmeticId = cosmetic.Id,
        SellingPrice = sellingPrice * orderItem.Value,
        Quantity = orderItem.Value,
        OrderId = order.Id
      });
    }

    if (request.CouponId is not null)
    {
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.CouponId);
      totalPrice = (totalPrice * (100m - (decimal)coupon.DiscountAmount)) / 100m;
    }
    order.TotalPrice = totalPrice;
    order.SubTotal = subTotal;
    order.OrderItems = orderItems;
    order.Status = OrderStatus.PENDING;

    await _unitOfWork.Orders.CreateAsync(order);
    await _unitOfWork.CompleteAsync();

    var invoiceStrategy = _invoiceGenerateStrategies.OfType<WalkInInvoiceStrategy>().FirstOrDefault();
    var invoiceByte = await invoiceStrategy.GenerateAsync(order, _unitOfWork);
    if (invoiceByte.Length == 0)
    {
      var error = _errorFactory.CreateFileCreatedFailed("Invoice");
      return Result<OrderResponse>.Failure([error.err], error.statusCode);
    }

    var orderResponse = MapToOrderResponse(order);
    orderResponse.CustomerId = customer.Id;
    orderResponse.Invoice = invoiceByte;

    return Result<OrderResponse>.Success(orderResponse, StatusCodes.Status200OK);
  }

  private async Task<Result<Cart>> ValidateOrderRequest(CreateOnlineOrderRequest request)
  {
    // Validate cart exists and has items
    var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(request.CartId);
    if (cart == null || !cart.CartItems.Any())
    {
      return Result<Cart>.Failure(
        [new Error("Order.Invalid", "Cart not found or empty")],
        StatusCodes.Status400BadRequest);
    }

    // Validate cart belongs to current user
    if (cart.CustomerId != _claimsService.CurrentUserId)
    {
      return Result<Cart>.Failure(
        [new Error("Order.Unauthorized", "Not authorized to access this cart")],
        StatusCodes.Status403Forbidden);
    }

    // Validate addresses
    if (string.IsNullOrEmpty(request.ShippingAddress) ||
        string.IsNullOrEmpty(request.BillingAddress))
    {
      return Result<Cart>.Failure(
        [new Error("Order.Invalid", "Shipping and billing addresses are required")],
        StatusCodes.Status400BadRequest);
    }

    return Result<Cart>.Success(cart, StatusCodes.Status200OK);
  }

  private async Task<Result<bool>> CheckAndDeductInventory(Cart cart)
  {
    var requiredQuantities = cart.CartItems
      .GroupBy(ci => ci.CosmeticId)
      .ToDictionary(g => g.Key, g => g.Sum(ci => ci.Quantity));

    // Keep track of batches to update to avoid multiple database calls
    var batchesToUpdate = new List<Batch>();

    foreach (var (cosmeticId, quantityNeeded) in requiredQuantities)
    {
      var availableBatches = await _unitOfWork.Batches.GetListByAnyId(e => e.CosmeticId == cosmeticId, 2);

      // Filter non-expired batches and order by FIFO principle
      availableBatches = availableBatches
        .Where(b => b.ExpirationDate >= DateOnly.FromDateTime(DateTime.UtcNow))
        .OrderBy(b => b.ExportedDate)
        .ToList();

      if (!availableBatches.Any())
      {
        return Result<bool>.Failure(
          [new Error("Order.ExpiredStock", "All available stock has expired")],
          StatusCodes.Status400BadRequest);
      }

      int totalAvailable = availableBatches.Sum(b => b.Quantity);
      if (totalAvailable < quantityNeeded)
      {
        return Result<bool>.Failure(
          [new Error("Order.InsufficientStock", "Not enough stock available")],
          StatusCodes.Status400BadRequest);
      }

      // Deduct from batches (FIFO)
      int remainingQuantity = quantityNeeded;
      foreach (var batch in availableBatches)
      {
        if (remainingQuantity <= 0) break;

        int deducted = Math.Min(batch.Quantity, remainingQuantity);
        batch.Quantity -= deducted;
        remainingQuantity -= deducted;

        batchesToUpdate.Add(batch);
      }
    }

    // Update all batches at once
    foreach (var batch in batchesToUpdate)
    {
      _unitOfWork.Batches.Update(batch);
    }

    return Result<bool>.Success(true, StatusCodes.Status200OK);
  }

  private ShippingDetails CalculateShippingDetails(IEnumerable<CartItem> cartItems)
  {
    var totalWeight = 0;
    var totalLength = 0;
    var totalWidth = 0;
    var totalHeight = 0;

    foreach (var cartItem in cartItems)
    {
      totalWeight += cartItem.Cosmetic.Weight * cartItem.Quantity;
      totalLength = Math.Max(totalLength, cartItem.Cosmetic.Length);
      totalWidth = Math.Max(totalWidth, cartItem.Cosmetic.Width);
      totalHeight += cartItem.Cosmetic.Height * cartItem.Quantity; // Stack height
    }

    return new ShippingDetails { Weight = totalWeight, Length = totalLength, Width = totalWidth, Height = totalHeight };
  }

  private Order CreateOrderEntity(CreateOnlineOrderRequest request, Cart cart)
  {
    return new Order
    {
      Id = Guid.NewGuid(),
      CustomerId = cart.CustomerId,
      CouponId = request.CouponId,
      OrderDate = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      ShippingAddress = request.ShippingAddress,
      BillingAddress = request.BillingAddress,
      TrackingNumber = null,
      Status = OrderStatus.PENDING,
      CreateAt = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      CreatedBy = cart.Customer.UserName,
      LastModified = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      LastModifiedBy = cart.Customer.UserName,
      OrderItems = cart.CartItems.Select(ci => new OrderItem
      {
        Cosmetic = ci.Cosmetic,
        CosmeticId = ci.CosmeticId,
        Quantity = ci.Quantity
      }).ToList(),
      // Set default delivery date to 7 days from now
      DeliveryDate = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow.AddDays(7))
    };
  }

  private decimal ConvertUsdToVnd(decimal usdAmount)
  {
    // Convert USD to VND using fixed rate
    // In a production environment, this should use a real-time exchange rate service
    return usdAmount * USD_TO_VND_RATE;
  }

  private async Task HandlePaymentMethod(string paymentMethod, Order order, OrderResponse orderResponse)
  {
    // Handle online payment
    if (paymentMethod == PaymentMethods.ONLINE)
    {
      var vnPayRequest = new VnPayPaymentRequestDto
      {
        OrderId = order.Id,
        PaymentMethod = PaymentMethods.ONLINE,
        Amount = (float)order.TotalPrice
      };

      var httpContext = _httpContextAccessor.HttpContext;
      if (httpContext != null)
      {
        var paymentUrlResult = _vnPayIntegrationService.CreatePaymentUrl(vnPayRequest, httpContext);
        if (paymentUrlResult.IsSuccess)
        {
          orderResponse.PaymentUrl = paymentUrlResult.Data;
        }
      }
    }

    // Handle COD payment (if needed in the future)
    // else if (paymentMethod == PaymentMethods.COD) { ... }
  }

  public async Task<Result<OrderResponse>> CompleteOrder(Guid orderId, string paymentStatus,
    PaymentReturnData paymentData)
  {
    try
    {
      var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
      if (order == null || order.Status != OrderStatus.PENDING)
      {
        return Result<OrderResponse>.Failure(
          new List<Error> { new Error("Order.NotFound", "Invalid order or wrong status") },
          StatusCodes.Status404NotFound);
      }

      // Determine order status based on VNPay's return paymentStatus (e.g. "00" means success)
      if (paymentStatus == "00")
      {
        order.Status = OrderStatus.CONFIRMED;

        // Create a payment record for this order
        var payment = new Payment
        {
          Id = Guid.NewGuid(),
          OrderId = order.Id,
          Method = PaymentMethods.ONLINE,
          TotalAmount = order.TotalPrice,
          Date = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
          TransactionId = paymentData.TransactionId // from VNPay return data
        };

        // Save payment record using your PaymentRepository
        _unitOfWork.Payments.Create(payment);

        // Optionally, clear the cart after successful payment
        await _unitOfWork.Carts.ClearCartItemsAsync(order.CustomerId);
      }
      else
      {
        order.Status = OrderStatus.FAILED;
      }

      order.LastModified = DateTime.UtcNow;
      order.LastModifiedBy = _claimsService.CurrentUserId.ToString();
      _unitOfWork.Orders.Update(order);

      var saved = await _unitOfWork.CompleteAsync();
      if (!saved)
      {
        return Result<OrderResponse>.Failure(
          new List<Error> { new Error("Order.Complete", "Failed to update order") },
          StatusCodes.Status500InternalServerError);
      }

      return Result<OrderResponse>.Success(
        MapToOrderResponse(order),
        StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<OrderResponse>.Failure(
        new List<Error> { new Error("Order.Complete", ex.Message) },
        StatusCodes.Status500InternalServerError);
    }
  }

  // 3. Get All Orders (Admin)
  public async Task<Result<List<OrderResponse>>> GetAllOrdersAsync()
  {
    try
    {
      var orders = await _unitOfWork.Orders.GetAllAsync();
      var response = orders.Select(MapToOrderResponse).ToList();
      return Result<List<OrderResponse>>.Success(response, StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<List<OrderResponse>>.Failure(
        [new Error("Order.GetAll", ex.Message)],
        StatusCodes.Status500InternalServerError);
    }
  }

  // 4. Get Customer Orders
  public async Task<Result<List<OrderResponse>>> GetOrdersByCustomerIdAsync(Guid customerId)
  {
    try
    {
      var orders = await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(customerId);
      var response = orders.Select(MapToOrderResponse).ToList();
      return Result<List<OrderResponse>>.Success(response, StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<List<OrderResponse>>.Failure(
        [new Error("Order.GetByCustomerId", ex.Message)],
        StatusCodes.Status500InternalServerError);
    }
  }

  // 5. Update Order Status (Admin)
  public async Task<Result<OrderResponse>> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusRequest request)
  {
    try
    {
      var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
      if (order == null)
      {
        return Result<OrderResponse>.Failure(
          [new Error("Order.NotFound", "Order not found")],
          StatusCodes.Status404NotFound);
      }

      order.Status = request.Status;
      order.LastModified = DateTime.UtcNow;
      order.LastModifiedBy = _claimsService.CurrentUserId.ToString();

      _unitOfWork.Orders.Update(order);
      var saved = await _unitOfWork.CompleteAsync();

      if (!saved)
      {
        return Result<OrderResponse>.Failure(
          [new Error("Order.Update", "Failed to update order")],
          StatusCodes.Status500InternalServerError);
      }

      return Result<OrderResponse>.Success(
        MapToOrderResponse(order),
        StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<OrderResponse>.Failure(
        [new Error("Order.Update", ex.Message)],
        StatusCodes.Status500InternalServerError);
    }
  }

  // 6. Delete Order (Admin)
  public async Task<Result<string>> DeleteOrderAsync(Guid orderId)
  {
    try
    {
      var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
      if (order == null)
      {
        return Result<string>.Failure(
          [new Error("Order.Delete", "Order not found")],
          StatusCodes.Status404NotFound);
      }

      _unitOfWork.Orders.Remove(order);
      var saved = await _unitOfWork.CompleteAsync();

      if (!saved)
      {
        return Result<string>.Failure(
          [new Error("Order.Delete", "Failed to delete order")],
          StatusCodes.Status500InternalServerError);
      }

      return Result<string>.Success(
        "Order deleted successfully",
        StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<string>.Failure(
        [new Error("Order.Delete", ex.Message)],
        StatusCodes.Status500InternalServerError);
    }
  }

  // 7. Cleanup Expired Orders (Background Job)
  public async Task CleanupExpiredOrders()
  {
    try
    {
      var expiryTime = DateTime.UtcNow.AddMinutes(-15);
      var expiredOrders = await _unitOfWork.Orders.GetExpiredPendingOrdersAsync(expiryTime);

      foreach (var order in expiredOrders)
      {
        order.Status = OrderStatus.EXPIRED;
        order.LastModified = DateTime.UtcNow;
        _unitOfWork.Orders.Update(order);
      }

      await _unitOfWork.CompleteAsync();
    }
    catch (Exception ex)
    {
      // Log error
      throw;
    }
  }

  private static OrderResponse MapToOrderResponse(Order order)
  {
    return new OrderResponse
    {
      Id = order.Id,
      CustomerId = order.CustomerId,
      CouponId = order.CouponId,
      SubTotal = order.SubTotal,
      TotalPrice = order.TotalPrice,
      OrderDate = order.OrderDate,
      ShippingAddress = order.ShippingAddress,
      BillingAddress = order.BillingAddress,
      TrackingNumber = order.TrackingNumber,
      DeliveryDate = order.DeliveryDate,
      Status = order.Status,
      CreateAt = order.CreateAt,
      CreatedBy = order.CreatedBy,
      LastModified = order.LastModified,
      LastModifiedBy = order.LastModifiedBy,
      OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
      {
        CosmeticId = oi.CosmeticId,
        Quantity = oi.Quantity,
        SellingPrice = oi.SellingPrice
      }).ToList()
    };
  }

  public static CreateGHNOrderRequest MapToCreateShippingOrderRequest(Order order, StoreData shopInfo,
    CreateOnlineOrderRequest onlineOrderRequest, ShippingDetails details, User customer, decimal codAmount,
    int paymentTypeId)
  {
    var request = new CreateGHNOrderRequest
    {
      PaymentTypeId = paymentTypeId, // Default value or map from Order.Payment if applicable
      Note = "Handle With Care", // Custom note
      RequiredNote = "CHOXEMHANGKHONGTHU", // Default required note
      FromName = shopInfo.Shops[1].Name, // Default sender name
      FromPhone = shopInfo.Shops[1].Phone, // Default sender phone
      FromAddress = shopInfo.Shops[1].Address, // Default sender address
      FromWardName = "Thao Dien", // Default ward name
      FromDistrictName = "Thanh Pho Thu Duc", // Default district name
      FromProvinceName = "Ho Chi Minh", // Default province name
      ReturnPhone = "", // Use customer's phone as return phone
      ReturnAddress = "", // Use billing address as return address
      ReturnDistrictId = null, // Set to null or map if applicable
      ReturnWardCode = "", // Set to empty or map if applicable
      ClientOrderCode = "", // Use Order ID as client order code
      ToName = customer.FirstName + customer.LastName, // Use customer's name as recipient name
      ToPhone = customer.PhoneNumber, // Use customer's phone as recipient phone
      ToAddress = order.ShippingAddress, // Use shipping address as recipient address
      ToWardCode = onlineOrderRequest.WardCode, // Default ward code or map if applicable
      ToDistrictId = onlineOrderRequest.DistrictId, // Default district ID or map if applicable
      CodAmount = (int)codAmount, // Use total price as COD amount
      Content = "Order from De Fleur", // Custom content
      Weight = details.Weight, // Calculate total weight
      Length = details.Length, // Default length or map if applicable
      Width = details.Width, // Default width or map if applicable
      Height = details.Height, // Default height or map if applicable
      PickStationId = 0, // Default pick station ID or map if applicable
      DeliverStationId = null, // Set to null or map if applicable
      InsuranceValue = (int)order.TotalPrice, // Use total price as insurance value
      ServiceId = 0, // Default service ID or map if applicable
      ServiceTypeId = 2, // Default service type ID or map if applicable
      Coupon = null, // Use coupon code if available
      PickShift = new List<int> { 1, 2 }, // Default pick shift or map if applicable
      Items = MapOrderItemsToGHNItems(order.OrderItems) // Map order items to GHN items
    };

    return request;
  }

  private static List<GHNOrderItem> MapOrderItemsToGHNItems(List<OrderItem> orderItems)
  {
    return orderItems.Select(item => new GHNOrderItem
    {
      Name = item.Cosmetic.Name, // Use cosmetic name as item name
      Code = item.CosmeticId.ToString(), // Use cosmetic ID as item code
      Quantity = item.Quantity, // Map quantity
      Price = (int)item.SellingPrice, // Map selling price
      Length = item.Cosmetic.Length, // Default length or map if applicable
      Width = item.Cosmetic.Width, // Default width or map if applicable
      Height = item.Cosmetic.Height, // Default height or map if applicable
      Weight = item.Cosmetic.Weight // Default weight or map if applicable
    }).ToList();
  }
}


// Helper class for shipping calculations
public class ShippingDetails
{
  public int Weight { get; set; }
  public int Length { get; set; }
  public int Width { get; set; }
  public int Height { get; set; }
}