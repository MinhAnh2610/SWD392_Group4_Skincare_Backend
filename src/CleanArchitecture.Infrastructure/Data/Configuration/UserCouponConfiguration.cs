using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class UserCouponConfiguration : IEntityTypeConfiguration<UserCoupon>

  {
    public void Configure(EntityTypeBuilder<UserCoupon> builder)
    {
      builder.HasKey(userCoupon => new { userCoupon.UserId, userCoupon.CouponId });
    }
  }
}