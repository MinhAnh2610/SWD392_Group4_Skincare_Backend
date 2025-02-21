using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IRoutineService
  {
    Task<Result<List<RoutineResponse>>> GetAllRoutinesAsync();

    Task<Result<List<RoutineResponse>?>> GetRoutinesBasedOnSkinType(Guid SkinTypeId);
  }
}
