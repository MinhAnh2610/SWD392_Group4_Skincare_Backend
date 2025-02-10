
namespace CleanArchitecture.Infrastructure.Repositories;

public class CartRepository : GenericRepository<Cart>
{
  public CartRepository(ApplicationDbContext context) : base(context)
  {
  }
}
