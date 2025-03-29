namespace CleanArchitecture.Domain.Entities;

public class Category : Entity<Guid>
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public List<SubCategory> SubCategories { get; set;} = new List<SubCategory>();
}
