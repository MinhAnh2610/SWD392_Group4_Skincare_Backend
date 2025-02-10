
namespace CleanArchitecture.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>
{
  public OrderRepository(ApplicationDbContext context) : base(context)
  {
  }
}
