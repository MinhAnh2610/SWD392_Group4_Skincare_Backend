using CleanArchitecture.Application.DTOs.FeedbackDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IFeedbackService
{
  Task<Result<List<FeedbackResponse>>> GetAllFeedbacksAsync();
  Task<Result<List<FeedbackResponse>>> GetFeedbacksByCustomerIdAsync(Guid customerId);
}
