using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
  {
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
      builder.HasKey(subCategory => subCategory.Id);

      builder.HasOne(subCategory => subCategory.Category)
          .WithMany(category => category.SubCategories)
          .HasForeignKey(subCategory => subCategory.CategoryId);

      builder.HasMany(subCategory => subCategory.CosmeticSubcategories)
        .WithOne(cosmeticSubCategory => cosmeticSubCategory.SubCategory)
        .HasForeignKey(cosmeticSubCategory => cosmeticSubCategory.SubCategoryId);
    }
  }
}
