using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.DTOs.RoutineStepDto;
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
        },
        RoutineSteps = routine.RoutineSteps.Select(routineStep => new RoutineStepResponse
        {
          CosmeticId = routineStep.CosmeticId,
          CosmeticName = routineStep.Cosmetic.Name,
          CosmeticNotice = routineStep.Cosmetic.Notice,
          CosmeticPrice = routineStep.Cosmetic.Price,
          StepNumber = routineStep.StepNumber,
        }).ToList(),
      };
    }

    public async Task<Result<List<RoutineResponse>?>> GetRoutinesBasedOnSkinType(Guid SkinTypeId)
    {
      try
      {
        var routines = await _unitOfWork.Routines.GetRoutineBySkinTypeAsync(SkinTypeId);

        if (routines == null)
          return Result<List<RoutineResponse>?>.Failure(new List<Error> { new Error("Routine.GetRoutines", "Cannot found routines") }, StatusCodes.Status404NotFound);

        var response = routines.Select(MapToRoutineResponse).ToList();

        return Result<List<RoutineResponse>?>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<List<RoutineResponse>?>.Failure(
            new List<Error> { new Error("Routine.GetRoutines", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }
  }
}
