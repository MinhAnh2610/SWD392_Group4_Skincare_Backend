namespace CleanArchitecture.Application.DTOs.BrandDto;

public class BrandResponse
{
  public Guid Id { get; set; }
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string WebsiteUrl { get; set; } = default!;
  public string LogoUrl { get; set; } = default!;
}
