namespace CleanArchitecture.Application.DTOs.CosmeticImageDto;

public class CosmeticImageResponse
{
  public Guid Id { get; set; }
  public Guid CosmeticId { get; set; }
  public string CosmeticName { get; set; } = default!;
  public string ImageUrl { get; set; } = default!;
}
