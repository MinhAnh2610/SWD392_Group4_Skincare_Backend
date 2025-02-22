using CleanArchitecture.Application.DTOs.Order;
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
    Task<Result<OrderResponse>> CheckOut(CheckOutRequest checkOutRequest);
    Task<Result<string>> DeleteOrderAsync(Guid orderId);
  }
}
