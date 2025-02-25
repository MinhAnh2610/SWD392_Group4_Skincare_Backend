using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class CartRepository : GenericRepository<Cart>, ICartRepository
  {
    public CartRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Cart?> GetByIdAsync(Guid id)
    {
      return await _context.Carts
          .Include(c => c.CartItems)
              .ThenInclude(ci => ci.Cosmetic)
                  .ThenInclude(cos => cos.CosmeticImages)
          .Include(c => c.Customer)
          .FirstOrDefaultAsync(c => c.Id == id);
    }

  }
}
