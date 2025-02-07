using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class SkinType : Entity<Guid>
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public bool IsDry { get; set; }
  public bool IsSensitive { get; set; }
  public bool IsUneven { get; set; }
  public bool IsWrinkle { get; set; }
}
