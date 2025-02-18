namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IOrderRepository : IGenericRepository<Order>
{
  Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
  Task<List<Order>> GetOrdersByStatusAsync(string status);
}
  