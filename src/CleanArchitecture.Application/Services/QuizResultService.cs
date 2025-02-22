using Castle.Core.Resource;
using CleanArchitecture.Application.DTOs.QuizAnswerDto;
using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.QuizResultDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class QuizResultService : IQuizResultService
{
  private readonly IUnitOfWork _unitOfWork;
  public QuizResultService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<List<QuizResultResponse>>> GetAllCustomerQuizResultsAsync()
  {
    try
    {
      var quizResults = await _unitOfWork.QuizResults.GetAllAsync();

      var results = quizResults.Select(qr => new QuizResultResponse
      {
        Id = qr.QuizId,
        CustomerId = qr.CustomerId,
        CustomerName = qr.Customer.UserName!,
        SkinTypeId = qr.SkinTypeId,
        SkinType = qr.SkinType.Name,
        QuizAnswers = qr.QuizAnswers.Select(qa => new QuizAnswerResponse
        {
          Id = qa.Id,
          QuestionId = qa.QuestionId,
          QuestionTitle = qa.Question.Title!,
          QuizResultId = qa.QuizResultId,
          SelectedOptions = qa.SelectedOptions
        }).ToList()
      }).ToList();

      return Result<List<QuizResultResponse>>.Success(results, StatusCodes.Status200OK);
    }
    catch (Exception)
    {
      return Result<List<QuizResultResponse>>.Failure(new List<Error>
      {
        new Error("QuizResult.ServerError", "Server Error Occurred.")
      }, StatusCodes.Status500InternalServerError);
    }
  }

  public async Task<Result<List<QuizResultResponse>>> GetUserQuizResultsAsync(Guid customerId)
  {
    try
    {
      var quizResults = await _unitOfWork.QuizResults.GetAllAsync();
      var customerResults = quizResults.Where(q => q.CustomerId == customerId);

      if (customerResults == null)
        return Result<List<QuizResultResponse>>.Failure(new List<Error>
        {
          new Error("QuizResult.NotFound", "Couldn't Load Customer's Quiz Results.")
        }, StatusCodes.Status404NotFound);

      var results = quizResults.Select(qr => new QuizResultResponse
      {
        Id = qr.QuizId,
        CustomerId = customerId,
        CustomerName = qr.Customer.UserName!,
        SkinTypeId = qr.SkinTypeId,
        SkinType = qr.SkinType.Name,
        QuizAnswers = qr.QuizAnswers.Select(qa => new QuizAnswerResponse
        {
          Id = qa.Id,
          QuestionId = qa.QuestionId,
          QuestionTitle = qa.Question.Title!,
          QuizResultId = qa.QuizResultId,
          SelectedOptions = qa.SelectedOptions
        }).ToList()
      }).ToList();

      return Result<List<QuizResultResponse>>.Success(results, StatusCodes.Status200OK);
    }
    catch (Exception)
    {
      return Result<List<QuizResultResponse>>.Failure(new List<Error>
      {
        new Error("QuizResult.ServerError", "Server Error Occurred.")
      }, StatusCodes.Status500InternalServerError);
    }
  }

  public async Task SaveQuizResultAsync(Guid customerId, Guid quizId, QuizSubmitRequest quizSubmission, RoutineResponse quizResponse)
  {
    var skinType = quizResponse.SkinType;
    var options = new List<QuestionOption>();
    var selectedOptionIds = quizSubmission.Answers
    .SelectMany(a => a.SelectedOptionIds)
    .ToList();

    foreach (var optionId in selectedOptionIds)
    {
      var option = await _unitOfWork.QuestionOptions.GetByIdAsync(optionId);
      options.Add(option);
    }

    var quizResult = new QuizResult
    {
      Id = Guid.NewGuid(),
      CustomerId = customerId,
      QuizId = quizId,
      SkinTypeId = skinType!.Id,
      QuizAnswers = quizSubmission.Answers.Select(a => new QuizAnswer
      {
        Id = Guid.NewGuid(),
        QuestionId = a.QuestionId,
        SelectedOptions = string.Join(", ", options
                .Where(o => a.SelectedOptionIds.Contains(o.Id)) 
                .Select(o => o.Content) 
            )!
      }).ToList()
    };

    await _unitOfWork.QuizResults.CreateAsync(quizResult);
    await _unitOfWork.CompleteAsync();
  }
}
