using CleanArchitecture.Application.DTOs.Cosmetic;

namespace CleanArchitecture.Application.DTOs.CosmeticImageDto;

public class CosmeticImageCosmeticResponse
{
  public Guid Id { get; set; }
  public CosmeticSimpleResponse? Cosmetic { get; set; } = default!;
  public string ImageUrl { get; set; } = default!;
}
