using CleanArchitecture.Application.DTOs.QuizAnswerDto;

namespace CleanArchitecture.Application.DTOs.QuizResultDto; 

public class QuizResultResponse
{
  public Guid Id { get; set; }
  public Guid CustomerId { get; set; }
  public string CustomerName { get; set; } = default!;
  public Guid SkinTypeId { get; set; }
  public string SkinType { get; set; } = default!;
  public List<QuizAnswerResponse>? QuizAnswers { get; set; }
}
