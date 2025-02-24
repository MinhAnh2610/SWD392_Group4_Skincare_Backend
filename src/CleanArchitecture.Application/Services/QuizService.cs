using CleanArchitecture.Application.DTOs.QuestionDto;
using CleanArchitecture.Application.DTOs.QuestionOptionDto;
using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.DTOs.RoutineStepDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class QuizService : IQuizService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IValidator<QuestionAddRequest> _addQuestionToQuizValidator;
  private readonly IValidator<QuestionUpdateRequest> _updateQuestionValidator;
  public QuizService(IUnitOfWork unitOfWork, IErrorFactory errorFactory, IValidator<QuestionAddRequest> addQuestionToQuizValidator, IValidator<QuestionUpdateRequest> updateRequestValidator)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _addQuestionToQuizValidator = addQuestionToQuizValidator;
    _updateQuestionValidator = updateRequestValidator;
  }

  public async Task<Result<bool>> AddQuestionToQuizAsync(Guid quizId, QuestionAddRequest request)
  {
    var validationResult = await _addQuestionToQuizValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var errors = validationResult.Errors
          .Select(e => new Error("ValidationError", e.ErrorMessage))
          .ToList();

      return Result<bool>.Failure(errors, StatusCodes.Status400BadRequest);
    }

    var quiz = await _unitOfWork.Quizs.GetByIdAsync(quizId);

    if (quiz == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(quiz));
      return Result<bool>.Failure([error.err], error.statusCode);
    }

    var questionTypes = await _unitOfWork.QuestionTypes.GetAllAsync();
    var questionType = questionTypes.FirstOrDefault(q => q.Name == request.QuestionType);

    if (questionType == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(questionType));
      return Result<bool>.Failure([error.err], error.statusCode);
    }

    quiz.Questions.Add(new Question
    {
      Title = request.Title,
      Description = request.Description,
      Instruction = request.Instruction,
      QuestionTypeId = questionType.Id,
      QuestionType = questionType,
      QuestionOptions = request.QuestionOptions.Select(qo => new QuestionOption
      {
        Content = qo.Content,
        Score = qo.Score,
      }).ToList(),
    });

    var result = await _unitOfWork.CompleteAsync();
    if (result)
    {
      return Result<bool>.Success(true, StatusCodes.Status200OK);
    }
    else
    {
      var error = _errorFactory.CreateDatabaseError(nameof(result));
      return Result<bool>.Failure([error.err], StatusCodes.Status500InternalServerError);
    }
  }

  public async Task<Result<QuizResponse>> GetQuizAsync()
  {
    var quizzes = await _unitOfWork.Quizs.GetAllAsync();
    var quiz = quizzes.FirstOrDefault();

    if (quiz == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(quiz));
      return Result<QuizResponse>.Failure([error.err], error.statusCode);
    }

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
    {
      var error = _errorFactory.CreateNotFoundError(nameof(quiz));
      return Result<List<RoutineResponse>?>.Failure([error.err], error.statusCode);
    }

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
    {
      var error = _errorFactory.CreateNotFoundError(nameof(skinType));
      return Result<List<RoutineResponse>?>.Failure([error.err], error.statusCode);
    }

    var routines = await _unitOfWork.Routines.GetRoutineBySkinTypeAsync(skinType.Id);

    if (routines == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(routines));
      return Result<List<RoutineResponse>?>.Failure([error.err], error.statusCode);
    }

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

  public async Task<Result<bool>> RemoveQuestionFromQuizAsync(Guid quizId, Guid questionId)
  {
    var quiz = await _unitOfWork.Quizs.GetByIdAsync(quizId);

    if (quiz == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(quiz));
      return Result<bool>.Failure([error.err], error.statusCode);
    }

    var question = await _unitOfWork.Questions.GetByIdAsync(questionId);

    if (question == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(question));
      return Result<bool>.Failure([error.err], error.statusCode);
    }

    quiz.Questions.Remove(question);

    _unitOfWork.Questions.Remove(question);
    await _unitOfWork.CompleteAsync();

    return Result<bool>.Success(true, StatusCodes.Status200OK);
  }

  public async Task<Result<bool>> UpdateQuestionAsync(Guid questionId, QuestionUpdateRequest request)
  {
    var validationResult = await _updateQuestionValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var errors = validationResult.Errors
          .Select(e => new Error("ValidationError", e.ErrorMessage))
          .ToList();

      return Result<bool>.Failure(errors, StatusCodes.Status400BadRequest);
    }

    var question = await _unitOfWork.Questions.GetByIdAsync(questionId);

    if (question == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(question));
      return Result<bool>.Failure([error.err], error.statusCode);
    }

    var questionTypes = await _unitOfWork.QuestionTypes.GetAllAsync();
    var questionType = questionTypes.FirstOrDefault(q => q.Name == request.QuestionType);

    if (questionType == null)
    {
      var error = _errorFactory.CreateNotFoundError(nameof(questionType));
      return Result<bool>.Failure([error.err], error.statusCode);
    }

    question.Title = request.Title;
    question.Description = request.Description;
    question.Instruction = request.Instruction;
    question.Section = request.Section;
    question.QuestionTypeId = questionType.Id;

    // Remove all existing options
    question.QuestionOptions!.Clear();

    // Add new options
    foreach (var newOption in request.QuestionOptions)
    {
      question.QuestionOptions.Add(new QuestionOption
      {
        Id = Guid.NewGuid(),
        Content = newOption.Content,
        Score = newOption.Score,
        QuestionId = question.Id
      });
    }

    _unitOfWork.Questions.Update(question);
    var result = await _unitOfWork.CompleteAsync();
    if (result)
    {
      return Result<bool>.Success(true, StatusCodes.Status200OK);
    }
    else
    {
      var error = _errorFactory.CreateDatabaseError(nameof(result));
      return Result<bool>.Failure([error.err], StatusCodes.Status500InternalServerError);
    }
  }
}
