using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class RoutineService : IRoutineService
  {
    private readonly IUnitOfWork _unitOfWork;

    public RoutineService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<RoutineResponse>>> GetAllRoutinesAsync()
    {
      try
      {
        var routines = await _unitOfWork.Routines.GetAllAsync();
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
        SkinType = new SkinTypeResponse
        {
          Id = routine.SkinType.Id,
          Name = routine.SkinType.Name,
          Description = routine.SkinType.Description,
          IsDry = routine.SkinType.IsDry,
          IsSensitive = routine.SkinType.IsSensitive,
          IsUneven = routine.SkinType.IsUneven,
          IsWrinkle = routine.SkinType.IsWrinkle
        }
      };
    }

    public Task<Result<RoutineResponse>> GetRoutineBasedOnSkinType(Guid SkinTypeId)
    {
      throw new NotImplementedException();
    }
  }
}
