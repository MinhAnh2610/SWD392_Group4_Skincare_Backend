namespace CleanArchitecture.Application.DTOs.SkinTypeDto;

public class SkinTypeResponse
{
  public Guid Id { get; set; }
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public bool IsDry { get; set; }
  public bool IsSensitive { get; set; }
  public bool IsUneven { get; set; }
  public bool IsWrinkle { get; set; }
}
