using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class OrderService : IOrderService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IErrorFactory _errorFactory;

    public OrderService(IUnitOfWork unitOfWork, IErrorFactory errorFactory)
    {
      _unitOfWork = unitOfWork;
      _errorFactory = errorFactory;
    }

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
          new List<Error> { new Error("Order.GetAll", ex.Message) },
          StatusCodes.Status500InternalServerError
        );
      }
    }

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
          new List<Error> { new Error("Order.GetByCustomerId", ex.Message) },
          StatusCodes.Status500InternalServerError
        );
      }
    }

    public async Task<Result<OrderResponse>> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusRequest request)
    {
      var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
      if (order == null)
      {
        var error = _errorFactory.CreateNotFoundError("Order");
        return Result<OrderResponse>.Failure([error.err], error.statusCode);
      }

      // Update the status and last modified date
      order.Status = request.Status;
      order.LastModified = DateTime.UtcNow;

      _unitOfWork.Orders.Update(order);

      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Order");
        return Result<OrderResponse>.Failure([error.err], error.statusCode);
      }

      var response = MapToOrderResponse(order);
      return Result<OrderResponse>.Success(response, StatusCodes.Status200OK);
    }

    public async Task<Result<OrderResponse>> CheckOut(CheckOutRequest checkOutRequest)
    {
      try
      {
        var cart = await _unitOfWork.Carts.GetByIdAsync(checkOutRequest.CartId);
        if (cart == null)
        {
          return Result<OrderResponse>.Failure(
            new List<Error> { new Error("Order.NotFound", "Order not found") },
            StatusCodes.Status404NotFound
          );
        }

        if (cart.CartItems != null && cart.CartItems.Count == 0)
        {
          return Result<OrderResponse>.Failure(
            new List<Error> { new Error("Order.CheckOut", "Cart is empty") },
            StatusCodes.Status400BadRequest
          );
        }

        if (cart.Customer.Id != checkOutRequest.UserId)
        {
          return Result<OrderResponse>.Failure(
            new List<Error> { new Error("Order.CheckOut", "Customer Id is not valid") },
            StatusCodes.Status400BadRequest
          );
        }

        if (checkOutRequest.ShippingAddress == null)
        {
          return Result<OrderResponse>.Failure(
            new List<Error> { new Error("Order.CheckOut", "Shipping Address is required") },
            StatusCodes.Status400BadRequest
          );
        }

        if (checkOutRequest.BillingAddress == null)
        {
          return Result<OrderResponse>.Failure(
            new List<Error> { new Error("Order.CheckOut", "Billing Address is required") },
            StatusCodes.Status400BadRequest
          );
        }

        Order order = new Order
        {
          CustomerId = cart.Customer.Id,
          CouponId = checkOutRequest.CouponId,
          SubTotal = cart.TotalPrice,
          TotalPrice = cart.TotalPrice,
          OrderDate = DateTime.UtcNow,
          ShippingAddress = checkOutRequest.ShippingAddress,
          BillingAddress = checkOutRequest.BillingAddress,
          TrackingNumber = Guid.NewGuid().ToString(),
          Status = "Pending",
          CreateAt = DateTime.UtcNow,
          CreatedBy = cart.Customer.NormalizedUserName,
          LastModified = DateTime.UtcNow,
          LastModifiedBy = cart.Customer.NormalizedUserName
        };

        var response = MapToOrderResponse(order);
        return Result<OrderResponse>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<OrderResponse>.Failure(
          new List<Error> { new Error("Order.UpdateStatus", ex.Message) },
          StatusCodes.Status500InternalServerError
        );
      }
    }

// New Delete Order Method
    public async Task<Result<string>> DeleteOrderAsync(Guid orderId)
    {
      try
      {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        if (order == null)
        {
          return Result<string>.Failure(
            new List<Error> { new Error("Order.Delete", "Order not found") },
            StatusCodes.Status404NotFound
          );
        }

        // Remove the order from the repository.
        _unitOfWork.Orders.Remove(order);
        var isSaved = await _unitOfWork.CompleteAsync();
        if (!isSaved)
        {
          var error = _errorFactory.CreateDatabaseError("Order");
          return Result<string>.Failure([error.err], error.statusCode);
        }

        return Result<string>.Success("Order deleted successfully", StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<string>.Failure(
          new List<Error> { new Error("Order.Delete", ex.Message) },
          StatusCodes.Status500InternalServerError
        );
      }
    }

    private static OrderResponse MapToOrderResponse(Order order)
    {
      return new OrderResponse
      {
        Id = order.Id,
        CustomerId = order.CustomerId,
        CouponId = (Guid)order.CouponId,
        SubTotal = order.SubTotal,
        TotalPrice = order.TotalPrice,
        OrderDate = order.OrderDate,
        ShippingAddress = order.ShippingAddress,
        BillingAddress = order.BillingAddress,
        TrackingNumber = order.TrackingNumber,
        DeliveryDate = order.DeliveryDate,
        Status = order.Status
      };
    }
  }
}