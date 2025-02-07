namespace CleanArchitecture.Domain.Entities;

public class CosmeticType : Entity<Guid>
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public List<Cosmetic> Cosmetics { get; set; } = new List<Cosmetic>();
}
