using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.GHN;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.DTOs.OrderDto;
using CleanArchitecture.Application.DTOs.VnPay;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

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
  public OrderService(
      IUnitOfWork unitOfWork,
      IErrorFactory errorFactory,
      IClaimsService claimsService,
      IGHNService ghnService,
      ITimeZoneService timeZoneService,
      IVnPayIntegrationService vnPayIntegrationService,
      IHttpContextAccessor httpContextAccessor)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _claimsService = claimsService;
    _ghnService = ghnService;
    _timeZoneService = timeZoneService;
    _vnPayIntegrationService = vnPayIntegrationService;
    _httpContextAccessor = httpContextAccessor;
  }


  // 1. Initiate Order (First step of checkout)
  public async Task<Result<OrderResponse>> InitiateOrder(CreateOrderRequest request)
  {
    try
    {
      #region Validate Request
      // Validate cart exists and has items
      var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(request.CartId);
      if (cart == null || !cart.CartItems.Any())
      {
        return Result<OrderResponse>.Failure(
            [new Error("Order.Invalid", "Cart not found or empty")],
            StatusCodes.Status400BadRequest);
      }

      // Validate cart belongs to current user
      if (cart.CustomerId != _claimsService.CurrentUserId)
      {
        return Result<OrderResponse>.Failure(
            [new Error("Order.Unauthorized", "Not authorized to access this cart")],
            StatusCodes.Status403Forbidden);
      }

      // Validate addresses
      if (string.IsNullOrEmpty(request.ShippingAddress) ||
          string.IsNullOrEmpty(request.BillingAddress))
      {
        return Result<OrderResponse>.Failure(
            [new Error("Order.Invalid", "Shipping and billing addresses are required")],
            StatusCodes.Status400BadRequest);
      }
      #endregion

      #region Deduct cosmetic quantity from batches (check the availability of batches)
      var requiredQuantities = cart.CartItems
        .GroupBy(ci => ci.CosmeticId)
        .ToDictionary(g => g.Key, g => g.Sum(ci => ci.Quantity));

      foreach (var (cosmeticId, quantityNeeded) in requiredQuantities)
      {
        var availableBatches = await _unitOfWork.Batches.GetListByAnyId(e => e.CosmeticId == cosmeticId, 2);

        // get only the non-expired batches
        availableBatches = availableBatches
          .Where(b => b.ExpirationDate >= DateOnly.FromDateTime(DateTime.UtcNow)) // Skip expired batches
          .OrderBy(b => b.ExportedDate)
          .ToList();

        if (!availableBatches.Any())
        {
          return Result<OrderResponse>.Failure(
              [new Error("Order.ExpiredStock", "All available stock has expired")],
              StatusCodes.Status400BadRequest);
        }

        int totalAvailable = availableBatches.Sum(b => b.Quantity);
        if (totalAvailable < quantityNeeded)
        {
          return Result<OrderResponse>.Failure(
              [new Error("Order.InsufficientStock", "Not enough stock available")],
              StatusCodes.Status400BadRequest);
        }

        // Deduct from batches (FIFO)
        foreach (var batch in availableBatches.OrderBy(b => b.ExportedDate))
        {
          if (quantityNeeded <= 0) break;

          int deducted = Math.Min(batch.Quantity, quantityNeeded);
          batch.Quantity -= deducted;
          //quantityNeeded -= deducted;

          _unitOfWork.Batches.Update(batch);
        }
      }
      #endregion

      #region Calculate the total shipping weight and dimesions
      var totalWeight = 0;
      var totalLength = 0;
      var totalWidth = 0;
      var totalHeight = 0;

      foreach (var cartItem in cart.CartItems)
      {
        totalWeight += cartItem.Cosmetic.Weight * cartItem.Quantity;
        totalLength = Math.Max(totalLength, cartItem.Cosmetic.Length);
        totalWidth = Math.Max(totalWidth, cartItem.Cosmetic.Width);
        totalHeight += cartItem.Cosmetic.Height * cartItem.Quantity; // Stack height
      }
      #endregion

      #region Payment methods business flow
      //string ghnOrderId = null;
      //if (request.PaymentMethod == PaymentMethods.COD)
      //{
      //  var ghnRequest = new CreateGHNOrderRequest
      //  {
      //    ServiceId = 53320, // Example service ID
      //    FromDistrictId = 1452, // Your shop’s district ID
      //    ToDistrictId = request.DistrictId, // Customer's district
      //    Weight = totalWeight,
      //    Length = totalLength,
      //    Width = totalWidth,
      //    Height = totalHeight,
      //    PaymentTypeId = 2, // 2 = COD, 1 = Prepaid
      //    Items = cart.CartItems.Select(ci => new GHNOrderItemRequest
      //    {
      //      Name = ci.Cosmetic.Name,
      //      Quantity = ci.Quantity,
      //      Price = ci.Cosmetic.Price,
      //      Code = ci.CosmeticId.ToString(),
      //      Weight = ci.Cosmetic.Weight,
      //      Height = ci.Cosmetic.Height,
      //      Width = ci.Cosmetic.Width,
      //      Length = ci.Cosmetic.Length
      //    }).ToList()
      //  };

      //  var deliveryResponse = await _ghnService.CreateShippingOrderAsync(ghnRequest);
      //  if (!deliveryResponse.IsSuccess)
      //  {
      //    return Result<OrderResponse>.Failure(
      //        [new Error("Order.Delivery", "Failed to initiate delivery")],
      //        StatusCodes.Status500InternalServerError);
      //  }

      //  ghnOrderId = deliveryResponse.GHNOrderId;
      //}
      #endregion

      // Create order in PENDING state
      var order = new Order
      {
        Id = Guid.NewGuid(),
        CustomerId = cart.CustomerId,
        CouponId = request.CouponId,
        SubTotal = cart.TotalPrice,
        TotalPrice = cart.TotalPrice, // apply coupon discount if needed
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
          CosmeticId = ci.CosmeticId,
          Quantity = ci.Quantity
        }).ToList(),
        // Set default delivery date to 7 days from now
        DeliveryDate = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow.AddDays(7))
      };


      // Save order
      _unitOfWork.Orders.Create(order);
      var saved = await _unitOfWork.CompleteAsync();

      if (!saved)
      {
        return Result<OrderResponse>.Failure(
            [new Error("Order.Create", "Failed to save order")],
            StatusCodes.Status500InternalServerError);
      }
      var orderResponse = MapToOrderResponse(order);


      // If the payment method is ONLINE, generate a payment URL.
      if (request.PaymentMethod == PaymentMethods.ONLINE)
      {
        // Create a VNPay payment request DTO using the order details.
        var vnPayRequest = new VnPayPaymentRequestDto
        {
          OrderId = order.Id,
          PaymentMethod = PaymentMethods.ONLINE,
          Amount = (float)order.TotalPrice  // Make sure your amount type is compatible
        };

        // Get HttpContext from the accessor.
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
          var paymentUrlResult = _vnPayIntegrationService.CreatePaymentUrl(vnPayRequest, httpContext);
          if (paymentUrlResult.IsSuccess)
          {
            // Add the generated URL to your order response.
            orderResponse.PaymentUrl = paymentUrlResult.Data;
          }
          else
          {
            // Optionally log or handle error
          }
        }
      }

      
      //TO DO FOR ANH TAN


      //for(int i = 0; i < _invoiceGenerateStrategies.Count(); i++)
      //{
      //  var nameOfStrat = _invoiceGenerateStrategies.ElementAt(i).GetType().Name;
      //  if (nameOfStrat.Contains("online", StringComparison.OrdinalIgnoreCase))
      //  {
      //    var invoice = _invoiceGenerateStrategies.ElementAt(i).Generate(order);
      //    orderResponse.Invoice = invoice;
      //    break;
      //  }
      //} 

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

  public async Task<Result<OrderResponse>> CompleteOrder(Guid orderId, string paymentStatus, PaymentReturnData paymentData)
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
        order.Payment = payment;

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
      LastModifiedBy = order.LastModifiedBy
    };
  }
}