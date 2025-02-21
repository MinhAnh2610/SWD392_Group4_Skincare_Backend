using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IRoutineService
  {
    Task<Result<List<RoutineResponse>>> GetAllRoutinesAsync();

    Task<Result<RoutineResponse>> GetRoutineBasedOnSkinType(Guid SkinTypeId);
  }
}
