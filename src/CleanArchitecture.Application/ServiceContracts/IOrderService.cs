using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.DTOs.OrderDto;
using CleanArchitecture.Application.DTOs.VnPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IOrderService
  {
    Task<Result<List<OrderResponse>>> GetAllOrdersAsync();
    Task<Result<List<OrderResponse>>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task<Result<OrderResponse>> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusRequest request);
    Task<Result<string>> DeleteOrderAsync(Guid orderId);
    Task<Result<OrderResponse>> InitiateOrder(CreateOrderRequest checkOutRequest);
    Task<Result<OrderResponse>> CompleteOrder(Guid orderId, string paymentStatus, PaymentReturnData paymentData);
    Task CleanupExpiredOrders();

  }
}
