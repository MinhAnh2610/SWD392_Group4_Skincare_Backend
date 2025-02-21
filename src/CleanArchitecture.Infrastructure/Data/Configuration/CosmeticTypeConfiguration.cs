using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class CosmeticTypeConfiguration : IEntityTypeConfiguration<CosmeticType>
  {
    public void Configure(EntityTypeBuilder<CosmeticType> builder)
    {
      builder.HasKey(cosmeticType => cosmeticType.Id);

      builder.HasMany(cosmeticType => cosmeticType.Cosmetics)
          .WithOne(cosmetic => cosmetic.CosmeticType)
          .HasForeignKey(cosmetic => cosmetic.CosmeticTypeId);
    }
  }
}
