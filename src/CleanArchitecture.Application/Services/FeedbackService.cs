using CleanArchitecture.Application.DTOs.FeedbackDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class FeedbackService : IFeedbackService
{
  private readonly IUnitOfWork _unitOfWork;
  public FeedbackService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<FeedbackResponse>>> GetFeedbacksAsync()
  {
    var result = await _unitOfWork.Feedbacks.GetAllFeedbacksAsync();

    return Result<List<FeedbackResponse>>.Success(result.Select(f => new FeedbackResponse
    {
      Id = f.Id,
      Content = f.Content,
      Rating = f.Rating,
      CustomerName = f.Customer.UserName,
      CustomerId = f.CustomerId,
    }).ToList(), StatusCodes.Status200OK);
  }
  
  public async Task<Result<List<FeedbackResponse>>> GetFeedbacksByCustomerIdAsync(Guid customerId)
  {
    try
    {
      var feedbacks = await _unitOfWork.Feedbacks.GetFeedbacksByCustomerIdAsync(customerId);
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
      CustomerId = feedback.CustomerId,
      Content = feedback.Content,
      Rating = feedback.Rating
    };
  }
}
