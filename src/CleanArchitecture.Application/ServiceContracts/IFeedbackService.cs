using CleanArchitecture.Application.DTOs.FeedbackDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IFeedbackService
  {
    Task<Result<List<FeedbackResponse>>> GetAllFeedbacksAsync();
    Task<Result<List<FeedbackResponse>>> GetFeedbacksByCustomerIdAsync(Guid customerId);
  }
}
