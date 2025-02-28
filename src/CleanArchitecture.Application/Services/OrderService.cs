using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.DTOs.OrderDto;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

public class OrderService : IOrderService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IClaimsService _claimsService;

  public OrderService(
      IUnitOfWork unitOfWork,
      IErrorFactory errorFactory,
      IClaimsService claimsService)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _claimsService = claimsService;
  }

  // 1. Initiate Order (First step of checkout)
  public async Task<Result<OrderResponse>> InitiateOrder(CreateOrderRequest request)
  {
    try
    {
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

      // Create order
      var order = new Order
      {
        Id = Guid.NewGuid(),
        CustomerId = cart.CustomerId,
        CouponId = request.CouponId,
        SubTotal = cart.TotalPrice,
        TotalPrice = cart.TotalPrice, // Apply coupon discount if needed
        OrderDate = DateTime.UtcNow,
        ShippingAddress = request.ShippingAddress,
        BillingAddress = request.BillingAddress,
        TrackingNumber = null,
        Status = OrderStatus.PENDING,
        CreateAt = DateTime.UtcNow,
        CreatedBy = cart.Customer.UserName,
        LastModified = DateTime.UtcNow,
        LastModifiedBy = cart.Customer.UserName,
        OrderItems = cart.CartItems.Select(ci => new OrderItem
        {
          CosmeticId = ci.CosmeticId,
          Quantity = ci.Quantity
        }).ToList()
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

      return Result<OrderResponse>.Success(
          MapToOrderResponse(order),
          StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<OrderResponse>.Failure(
          [new Error("Order.Create", ex.Message)],
          StatusCodes.Status500InternalServerError);
    }
  }

  // 2. Complete Order (After payment)
  public async Task<Result<OrderResponse>> CompleteOrder(Guid orderId, string paymentStatus)
  {
    try
    {
      var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
      if (order == null || order.Status != OrderStatus.PENDING)
      {
        return Result<OrderResponse>.Failure(
            [new Error("Order.NotFound", "Invalid order or wrong status")],
            StatusCodes.Status404NotFound);
      }

      // Update order status based on payment result
      order.Status = paymentStatus == "00" ? OrderStatus.CONFIRMED : OrderStatus.FAILED;
      order.LastModified = DateTime.UtcNow;
      order.LastModifiedBy = _claimsService.CurrentUserId.ToString();

      if (order.Status == OrderStatus.CONFIRMED)
      {
        // Clear cart after successful payment
        await _unitOfWork.Carts.ClearCartItemsAsync(order.CustomerId);
      }

      _unitOfWork.Orders.Update(order);
      var saved = await _unitOfWork.CompleteAsync();

      if (!saved)
      {
        return Result<OrderResponse>.Failure(
            [new Error("Order.Complete", "Failed to update order")],
            StatusCodes.Status500InternalServerError);
      }

      return Result<OrderResponse>.Success(
          MapToOrderResponse(order),
          StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<OrderResponse>.Failure(
          [new Error("Order.Complete", ex.Message)],
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