using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
  {
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
      builder.Property(c => c.PointRequired).IsRequired();
    }
  }
}