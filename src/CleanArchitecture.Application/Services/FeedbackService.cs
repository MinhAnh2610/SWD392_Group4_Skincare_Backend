// File: Application/Services/FeedbackService.cs
using CleanArchitecture.Application.DTOs.FeedbackDto;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class FeedbackService : IFeedbackService
  {
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository)
    {
      _feedbackRepository = feedbackRepository;
    }

    public async Task<Result<List<FeedbackResponse>>> GetAllFeedbacksAsync()
    {
      try
      {
        var feedbacks = await _feedbackRepository.GetAllAsync();
        var responses = feedbacks.Select(MapToFeedbackResponse).ToList();
        return Result<List<FeedbackResponse>>.Success(responses, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<List<FeedbackResponse>>.Failure(
            new List<Error> { new Error("Feedback.GetAll", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }

    public async Task<Result<List<FeedbackResponse>>> GetFeedbacksByCustomerIdAsync(Guid customerId)
    {
      try
      {
        var feedbacks = await _feedbackRepository.GetFeedbacksByCustomerIdAsync(customerId);
        var responses = feedbacks.Select(MapToFeedbackResponse).ToList();
        return Result<List<FeedbackResponse>>.Success(responses, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<List<FeedbackResponse>>.Failure(
            new List<Error> { new Error("Feedback.GetByCustomer", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }

    private static FeedbackResponse MapToFeedbackResponse(Feedback feedback)
    {
      return new FeedbackResponse
      {
        Id = feedback.Id,
        CosmeticId = feedback.CosmeticId,
        CustomerId = feedback.CustomerId,
        Content = feedback.Content,
        Rating = feedback.Rating,
        CreateAt = (DateTime)feedback.CreateAt,
        CreatedBy = feedback.CreatedBy,
        LastModified = feedback.LastModified,
        LastModifiedBy = feedback.LastModifiedBy
      };
    }
  }
}
