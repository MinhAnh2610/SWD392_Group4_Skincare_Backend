using CleanArchitecture.Application.DTOs.QuestionDto;

namespace CleanArchitecture.Application.DTOs.QuizDto;

public class QuizResponse
{
  public Guid Id { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public int TargetAgeFrom { get; set; }
  public int TargetAgeTo { get; set; }
  public List<QuestionResponse>? Questions { get; set; }
}
