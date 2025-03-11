namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IOrderRepository : IGenericRepository<Order>
{
  Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
  Task<List<Order>> GetOrdersByStatusAsync(string status);
  Task<List<Order>> GetExpiredPendingOrdersAsync(DateTime expiryTime);
  Task<Order?> GetOrderWithItemsAsync(Guid orderId);
  Task<List<Order>> GetAllOrdersWithItemsAsync();
}
  