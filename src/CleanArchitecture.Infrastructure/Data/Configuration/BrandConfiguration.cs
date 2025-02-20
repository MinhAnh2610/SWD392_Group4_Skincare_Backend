using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class BrandConfiguration : IEntityTypeConfiguration<Brand>
  {
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
      builder.HasKey(brand => brand.Id);

      builder.HasMany(brand => brand.Cosmetics)
          .WithOne(cosmetic => cosmetic.Brand);
    }
  }
}
