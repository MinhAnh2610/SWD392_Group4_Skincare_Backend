using CleanArchitecture.Application.DTOs.FeedbackDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IFeedbackService
{
  Task<Result<List<FeedbackResponse>>> GetFeedbacksAsync();
  Task<Result<List<FeedbackResponse>>> GetFeedbacksByCustomerIdAsync(Guid customerId);
}
