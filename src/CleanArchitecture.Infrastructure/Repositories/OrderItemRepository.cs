namespace CleanArchitecture.Infrastructure.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>
{
  public OrderItemRepository(ApplicationDbContext context) : base(context)
  {
  }
}
