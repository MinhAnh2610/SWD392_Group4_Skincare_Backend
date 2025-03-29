namespace CleanArchitecture.Domain.Entities;

public class SubCategory : Entity<Guid>
{
  public Guid CategoryId { get; set; }
  public Category Category { get; set; } = default!;
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public List<CosmeticSubCategory> CosmeticSubcategories { get; set; } = new List<CosmeticSubCategory>();
}
