using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class CartRepository : GenericRepository<Cart>, ICartRepository
  {
    public CartRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task ClearCartItemsAsync(Guid cartId)
    {
      var cartItems = await _context.CartItems
          .Where(ci => ci.CartId == cartId)
          .ToListAsync();

      _context.CartItems.RemoveRange(cartItems);
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

    public async Task<Cart?> GetCartWithItemsAsync(Guid cartId)
    {
      return await _context.Carts
          .Include(c => c.CartItems)
          .ThenInclude(ci => ci.Cosmetic)
          .FirstOrDefaultAsync(c => c.Id == cartId);
    }
  }
}
