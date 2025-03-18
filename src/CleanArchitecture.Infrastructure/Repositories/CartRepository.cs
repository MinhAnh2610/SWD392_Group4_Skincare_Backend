using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class CartRepository : GenericRepository<Cart>, ICartRepository
  {
    public CartRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<List<Cart>> GetAllAsync()
    {
      return await _context.Carts
        .Include(c => c.Customer)
        .Include(c => c.CartItems)
        .ThenInclude(ci => ci.Cosmetic)
        .ThenInclude(c => c.CosmeticImages)
        .ToListAsync();
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
    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
    {
      return await _context.Carts
        .Include(c => c.Customer)
        .Include(c => c.CartItems)
          .ThenInclude(ci => ci.Cosmetic)
            .ThenInclude(cos => cos.CosmeticImages)
        .FirstOrDefaultAsync(c => c.CustomerId == userId);
    }

    public async Task<Cart?> GetCartWithItemsAsync(Guid cartId)
    {
      return await _context.Carts
        .Include(c => c.Customer)
        .Include(c => c.CartItems)
        .ThenInclude(ci => ci.Cosmetic)
        .FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public async Task<Cart> GetCartByUserIdWithLockAsync(Guid userId)
    {
      return await _context.Carts
    .Include(c => c.CartItems)
    .FirstOrDefaultAsync(c => c.CustomerId == userId);
    }
  }
}