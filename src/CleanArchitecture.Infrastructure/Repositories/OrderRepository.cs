using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
  public OrderRepository(ApplicationDbContext context) : base(context)
  {
  }
  public async Task<List<Order>> GetAllOrdersWithItemsAsync()
  {
    return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Cosmetic)
            .OrderByDescending(o => o.CreateAt)
            .ToListAsync();
  }
  public async Task<List<Order>> GetExpiredPendingOrdersAsync(DateTime expiryTime)
  {
    return await _context.Orders
        .Where(o => o.Status == "PENDING_PAYMENT" && o.CreateAt < expiryTime)
        .ToListAsync();
  }

  public async Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
  {
    return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Cosmetic)
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreateAt)
            .ToListAsync();
  }

  public async Task<List<Order>> GetOrdersByStatusAsync(string status)
  {
    return await _context.Orders
          .Include(o => o.OrderItems)
          .Where(o => o.Status == status)
          .OrderByDescending(o => o.CreateAt)
          .ToListAsync();
  }
  public async Task<Order?> GetOrderWithItemsAsync(Guid orderId)
  {
    return await _context.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Cosmetic)
        .FirstOrDefaultAsync(o => o.Id == orderId);
  }
}
