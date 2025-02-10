using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
  public CartRepository(ApplicationDbContext context) : base(context)
  {
  }
}
