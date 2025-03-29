namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class CosmeticSubCategoryConfiguration : IEntityTypeConfiguration<CosmeticSubCategory>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CosmeticSubCategory> builder)
    {
      builder.HasKey(cosmeticSubCategory => new { cosmeticSubCategory.SubCategoryId, cosmeticSubCategory.CosmeticId });

    }
  }
}
