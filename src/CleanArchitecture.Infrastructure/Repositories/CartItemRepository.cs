using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
{
  public CartItemRepository(ApplicationDbContext context) : base(context)
  {
  }
}
