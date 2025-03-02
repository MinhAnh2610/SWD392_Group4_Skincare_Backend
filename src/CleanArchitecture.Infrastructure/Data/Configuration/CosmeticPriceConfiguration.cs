using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class CosmeticPriceConfiguration : IEntityTypeConfiguration<CosmeticPrice>
  {
    public void Configure(EntityTypeBuilder<CosmeticPrice> builder)
    {
      builder.ToTable("CosmeticPrices");

      builder.HasKey(cp => cp.Id);

      builder.Property(cp => cp.Id).ValueGeneratedOnAdd();

      builder.HasOne(cp => cp.Cosmetic);

      builder.HasOne(cp => cp.Cosmetic)
        .WithMany(c => c.CosmeticPrices)
        .HasForeignKey(cp => cp.CosmeticId);

      builder.HasOne(cp => cp.Event)
        .WithMany(e => e.CosmeticPrices)
        .HasForeignKey(cp => cp.EventId);

      builder.Property(cp => cp.CreateAt)
        .HasColumnType("timestamp")
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

      builder.Property(cp => cp.StartDate)
        .HasColumnType("timestamp")
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

      builder.Property(cp => cp.EndDate)
        .HasColumnType("timestamp")
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

      builder.Property(cp => cp.LastModified)
        .HasColumnType("timestamp")
        .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
  }
}