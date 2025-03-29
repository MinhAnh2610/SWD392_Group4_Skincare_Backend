using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.DTOs.FeedbackDto;

public class FeedbackCosmeticResponse
{
  public Guid Id { get; set; }
  public CustomerDto? Customer { get; set; }
  public string? Content { get; set; }
  public decimal Rating { get; set; }
}
