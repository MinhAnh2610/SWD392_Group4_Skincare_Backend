namespace CleanArchitecture.Application.DTOs.RoutineStepDto;

public class RoutineStepResponse
{
  public Guid CosmeticId { get; set; }
  public string CosmeticName { get; set; } = default!;
  public string CosmeticNotice { get; set; } = default!;
  public decimal CosmeticPrice { get; set; } 
  public int StepNumber { get; set; }
}
