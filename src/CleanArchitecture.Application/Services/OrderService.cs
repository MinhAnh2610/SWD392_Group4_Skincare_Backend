// OrderService.cs
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class OrderService : IOrderService
  {
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
      _orderRepository = orderRepository;
    }

    public async Task<Result<List<OrderResponse>>> GetAllOrdersAsync()
    {
      try
      {
        var orders = await _orderRepository.GetAllAsync();
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
        var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
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
      try
      {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
          return Result<OrderResponse>.Failure(
              new List<Error> { new Error("Order.NotFound", "Order not found") },
              StatusCodes.Status404NotFound
          );
        }

        // Update the status and last modified date
        order.Status = request.Status;
        order.LastModified = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);

        var response = MapToOrderResponse(order);
        return Result < OrderResponse >.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<OrderResponse>.Failure(
            new List<Error> { new Error("Order.UpdateStatus", ex.Message) },
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
        CouponId = order.CouponId,
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
