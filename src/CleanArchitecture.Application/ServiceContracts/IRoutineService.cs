// File: Application/ServiceContracts/IRoutineService.cs
using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IRoutineService
  {
    Task<Result<List<RoutineResponse>>> GetAllRoutinesAsync();
  }
}
