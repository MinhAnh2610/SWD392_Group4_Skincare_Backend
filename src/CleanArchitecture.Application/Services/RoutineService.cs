// File: Application/Services/RoutineService.cs
using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class RoutineService : IRoutineService
  {
    private readonly IRoutineRepository _routineRepository;

    public RoutineService(IRoutineRepository routineRepository)
    {
      _routineRepository = routineRepository;
    }

    public async Task<Result<List<RoutineResponse>>> GetAllRoutinesAsync()
    {
      try
      {
        var routines = await _routineRepository.GetAllAsync();
        var responses = routines.Select(MapToRoutineResponse).ToList();
        return Result<List<RoutineResponse>>.Success(responses, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<List<RoutineResponse>>.Failure(
            new List<Error> { new Error("Routine.GetAll", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }

    private static RoutineResponse MapToRoutineResponse(Routine routine)
    {
      return new RoutineResponse
      {
        Id = routine.Id,
        Title = routine.Title,
        Period = routine.Period,
        SkinTypeId = routine.SkinTypeId
      };
    }
  }
}
