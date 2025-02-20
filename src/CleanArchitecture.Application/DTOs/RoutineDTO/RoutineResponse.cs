using CleanArchitecture.Application.DTOs.RoutineStepDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;

namespace CleanArchitecture.Application.DTOs.RoutineDTO
{
  public class RoutineResponse
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Period { get; set; } = default!;
    public SkinTypeResponse? SkinType { get; set; }
    public List<RoutineStepResponse>? RoutineSteps { get; set; }
  }
}
