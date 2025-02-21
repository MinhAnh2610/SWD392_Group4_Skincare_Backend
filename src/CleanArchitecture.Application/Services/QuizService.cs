using CleanArchitecture.Application.DTOs.QuestionDto;
using CleanArchitecture.Application.DTOs.QuestionOptionDto;
using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.DTOs.RoutineStepDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class QuizService : IQuizService
{
  public readonly IUnitOfWork _unitOfWork;
  public QuizService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<QuizResponse>> GetQuizAsync()
  {
    var quizzes = await _unitOfWork.Quizs.GetAllAsync();
    var quiz = quizzes.FirstOrDefault();

    if (quiz == null)
      return Result<QuizResponse>.Failure(new List<Error>
      {
        new Error("Quiz.NotFound", "Couldn't Load Skin Type Quiz.")
      }, StatusCodes.Status404NotFound);

    return Result<QuizResponse>.Success(new QuizResponse
    {
      Id = quiz.Id,
      Title = quiz.Title,
      Description = quiz.Description,
      TargetAgeTo = quiz.TargetAgeTo,
      TargetAgeFrom = quiz.TargetAgeFrom,
      Questions = quiz.Questions.Select(q => new QuestionResponse
      {
        Id = q.Id,
        Title = q.Title,
        Description = q.Description,
        Instruction = q.Instruction,
        Section = q.Section,
        QuestionType = q.QuestionType!.Name,
        QuestionOptions = q.QuestionOptions!.Select(qo => new QuestionOptionResponse
        {
          Id = qo.Id,
          Content = qo.Content,
          Score = qo.Score,
        }).ToList(),
      }).ToList()
    }, StatusCodes.Status200OK);
  }

  public async Task<Result<List<RoutineResponse>?>> ProcessQuizAsync(Guid id, QuizSubmitRequest request)
  {
    var quiz = await _unitOfWork.Quizs.GetByIdAsync(id);

    if (quiz == null)
      return Result<List<RoutineResponse>?>.Failure(new List<Error>
      {
        new Error("Quiz.NotFound", "Couldn't Found Skin Type Quiz.")
      }, StatusCodes.Status404NotFound);

    int oilinessScore = 0;
    int sensitivityScore = 0;
    int pigmentationScore = 0;
    int agingScore = 0;

    foreach (var response in request.Answers)
    {
      var question = quiz.Questions.FirstOrDefault(q => q.Id == response.QuestionId);
      if (question == null) continue;

      foreach (var selectedOptionId in response.SelectedOptionIds)
      {
        var option = question.QuestionOptions!.FirstOrDefault(o => o.Id == selectedOptionId);
        if (option == null) continue;

        // Categorize scores based on question section
        switch (question.Section!.ToLower())
        {
          case "oiliness":
            oilinessScore += option.Score;
            break;
          case "sensitivity":
            sensitivityScore += option.Score;
            break;
          case "pigmentation":
            pigmentationScore += option.Score;
            break;
          case "aging":
            agingScore += option.Score;
            break;
        }
      }
    }

    string oilinessType = oilinessScore > 0 ? "O" : "D";  // Oily vs Dry
    string sensitivityType = sensitivityScore > 0 ? "S" : "R";  // Sensitive vs Resistant
    string pigmentationType = pigmentationScore > 0 ? "P" : "N";  // Pigmented vs Non-Pigmented
    string agingType = agingScore > 0 ? "W" : "T";  // Wrinkle-Prone vs Tight

    var skinType = await _unitOfWork.SkinTypes.FindSkinTypeBasedOnBaumannAsync(oilinessType + sensitivityType + pigmentationType + agingType);

    if (skinType == null)
      return Result<List<RoutineResponse>?>.Failure(new List<Error>
      {
        new Error("SkinType.NotFound", "Couldn't Found Skin Type.")
      }, StatusCodes.Status404NotFound);

    var routines = await _unitOfWork.Routines.GetRoutineBySkinTypeAsync(skinType.Id);

    if (routines == null)
      return Result<List<RoutineResponse>?>.Failure(new List<Error> { new Error("Routine.GetRoutines", "Cannot found routines") }, StatusCodes.Status404NotFound);

    return Result<List<RoutineResponse>?>.Success(routines.Select(routine => new RoutineResponse
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
    }).ToList(), StatusCodes.Status200OK);
  }
}
