using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.HasKey(category => category.Id);

      builder.HasMany(category => category.SubCategories)
          .WithOne(subCategory => subCategory.Category)
          .HasForeignKey(subCategory => subCategory.CategoryId);
    }
  }
}
