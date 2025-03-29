using CleanArchitecture.Application.DTOs.FeedbackDto;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class FeedbackService : IFeedbackService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<FeedbackRequest> _createFeedbackValidator;
  private readonly IErrorFactory _errorFactory;
  public FeedbackService(IUnitOfWork unitOfWork, IValidator<FeedbackRequest> createFeedbackValidator, IErrorFactory errorFactory)
  {
    _unitOfWork = unitOfWork;
    _createFeedbackValidator = createFeedbackValidator;
    _errorFactory = errorFactory;
  }
  public async Task<Result<List<FeedbackResponse>>> GetAllFeedbacksAsync()
  {
    var result = await _unitOfWork.Feedbacks.GetAllAsync();

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

  public async Task<Result<FeedbackResponse>> CreateFeedbackAsync(FeedbackRequest request, Guid customerId, string username)
  {
    var validationResult = await _createFeedbackValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var errors = _errorFactory.CreateValidationError(nameof(request), validationResult);
      return Result<FeedbackResponse>.Failure(errors.errs, errors.statusCode);
    }

    var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(request.CosmeticId);
    if (cosmetic == null)
    {
      var error = new Error("NotFound", "Cosmetic not found.");
      return Result<FeedbackResponse>.Failure([error], StatusCodes.Status404NotFound);
    }

    var feedback = new Feedback
    {
      Id = Guid.NewGuid(),
      CosmeticId = request.CosmeticId,
      CustomerId = customerId,
      Rating = request.Rating,
      Content = request.Content
    };

    await _unitOfWork.Feedbacks.CreateAsync(feedback);
    await _unitOfWork.CompleteAsync();

    return Result<FeedbackResponse>.Success(new FeedbackResponse
    {
      Id = feedback.Id,
      Content = feedback.Content,
      CustomerId = feedback.CustomerId,
      CustomerName = username,
      Rating = feedback.Rating
    }, StatusCodes.Status200OK);
  }
}
