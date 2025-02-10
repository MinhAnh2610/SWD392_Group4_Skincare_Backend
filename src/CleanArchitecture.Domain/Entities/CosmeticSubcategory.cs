namespace CleanArchitecture.Domain.Entities;

public class CosmeticSubCategory
{
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public Guid SubCategoryId { get; set; }
  public SubCategory SubCategory { get; set; } = default!;
}
