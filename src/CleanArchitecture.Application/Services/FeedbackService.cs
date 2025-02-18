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
}
