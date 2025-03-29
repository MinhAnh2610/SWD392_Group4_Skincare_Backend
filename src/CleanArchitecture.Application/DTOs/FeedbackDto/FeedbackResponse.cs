using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.DTOs.FeedbackDto;

public class FeedbackResponse
{
  public Guid Id { get; set; }
  public Guid CustomerId { get; set; }
  public string? CustomerName { get; set; }
  public string? Content { get; set; }
  public decimal Rating { get; set; }
}
