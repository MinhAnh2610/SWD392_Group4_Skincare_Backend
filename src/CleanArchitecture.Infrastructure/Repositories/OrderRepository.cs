using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
  public OrderRepository(ApplicationDbContext context) : base(context)
  {
  }
}
