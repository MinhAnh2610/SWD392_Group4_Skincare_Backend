namespace CleanArchitecture.Domain.Entities;

public class CosmeticImage : Entity<Guid>
{
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public string ImageUrl { get; set; } = default!;
}
