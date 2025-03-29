using CleanArchitecture.Application.DTOs.Cosmetic;

namespace CleanArchitecture.Application.DTOs.CosmeticImageDto;

public class CosmeticSimpleResponse
{
  public Guid Id { get; set; }
  public string Name { get; set; } = default!;
  public decimal Price { get; set; }
}
