using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
  public OrderRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
  {
    return await _context.Orders
        .Where(o => o.CustomerId == customerId)
        .ToListAsync();
  }

  public async Task<List<Order>> GetOrdersByStatusAsync(string status)
  {
    return await _context.Orders
        .Where(o => o.Status == status)
        .ToListAsync();
  }
}
