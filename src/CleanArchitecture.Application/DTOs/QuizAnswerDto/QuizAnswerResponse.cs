namespace CleanArchitecture.Application.DTOs.QuizAnswerDto;

public class QuizAnswerResponse
{
  public Guid Id { get; set; }
  public Guid QuizResultId { get; set; }
  public Guid QuestionId { get; set; }
  public string QuestionTitle { get; set; } = default!;
  public string SelectedOptions { get; set; } = default!;
}
