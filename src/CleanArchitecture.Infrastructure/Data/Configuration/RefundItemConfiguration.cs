using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class RefundItemConfiguration : IEntityTypeConfiguration<RefundItem>
  {
    public void Configure(EntityTypeBuilder<RefundItem> builder)
    {
      builder.HasKey(refundItem => refundItem.Id);

      builder.HasOne(refundItem => refundItem.Refund)
          .WithMany(refund => refund.RefundItems)
          .HasForeignKey(refundItem => refundItem.RefundId)
          .IsRequired();

      builder.HasOne(refundItem => refundItem.Cosmetic)
          .WithMany(cosmetic => cosmetic.RefundItems)
          .HasForeignKey(refundItem => refundItem.CosmeticId)
          .IsRequired();
    }
  }
}
