namespace CleanArchitecture.Infrastructure.Repositories;

public class CartItemRepository : GenericRepository<CartItem>
{
  public CartItemRepository(ApplicationDbContext context) : base(context)
  {
  }
}
