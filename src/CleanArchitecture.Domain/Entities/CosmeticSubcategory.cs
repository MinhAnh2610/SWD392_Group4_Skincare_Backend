namespace CleanArchitecture.Domain.Entities;

public class CosmeticSubcategory
{
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public Guid SubCategoryId { get; set; }
  public SubCategory SubCategory { get; set; } = default!;
}
