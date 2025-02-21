using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class CosmeticImageConfiguration : IEntityTypeConfiguration<CosmeticImage>
  {
    public void Configure(EntityTypeBuilder<CosmeticImage> builder)
    {
      builder.HasKey(cosmeticImage => cosmeticImage.Id);

      builder.HasOne(cosmeticImage => cosmeticImage.Cosmetic)
          .WithMany(cosmetic => cosmetic.CosmeticImages)
          .HasForeignKey(cosmeticImage => cosmeticImage.CosmeticId);
    }
  }
}
