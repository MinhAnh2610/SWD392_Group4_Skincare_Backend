namespace CleanArchitecture.Application.DTOs.CosmeticTypeDto;

public class CosmeticTypeResponse
{
  public Guid Id { get; set; }
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
}
