namespace CleanArchitecture.Domain.Entities;

public class Brand : Entity<Guid>
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string WebsiteUrl { get; set; } = default!;
  public string LogoUrl { get; set; } = default!;
  public List<Cosmetic> Cosmetics { get; set; } = new List<Cosmetic>();
}
