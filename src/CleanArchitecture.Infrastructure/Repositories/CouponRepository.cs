using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
  public CouponRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<Coupon?> GetByIdAsync(Guid? id)
  {
    if (id is null)
      return null;
    
    return await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
  }
}
