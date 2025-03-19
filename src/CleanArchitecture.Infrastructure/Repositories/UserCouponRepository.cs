using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class UserCouponRepository : GenericRepository<UserCoupon>, IUserCouponRepository
  {
    public UserCouponRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserCoupon?> GetByIdAsync(Guid userId, Guid couponId)
    {
      var userCoupon = await _context.UserCoupons
        .Include(uc => uc.User)
        .Include(uc => uc.Coupon)
        .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CouponId == couponId);

      return userCoupon;
    }

    public async Task<List<UserCoupon>> GetUserCouponsAsync(User user)
    {
      var coupons = await _context.UserCoupons
        .Where(c => c.UserId == user.Id)
        .ToListAsync();

      return coupons;
    }
  }
}