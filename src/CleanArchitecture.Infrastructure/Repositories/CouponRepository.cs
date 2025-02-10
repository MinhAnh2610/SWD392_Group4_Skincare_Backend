
namespace CleanArchitecture.Infrastructure.Repositories;

public class CouponRepository : GenericRepository<Coupon>
{
  public CouponRepository(ApplicationDbContext context) : base(context)
  {
  }
}
