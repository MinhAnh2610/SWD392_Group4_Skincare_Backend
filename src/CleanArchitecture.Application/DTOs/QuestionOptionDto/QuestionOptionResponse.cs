namespace CleanArchitecture.Application.DTOs.QuestionOptionDto;

public class QuestionOptionResponse
{
  public Guid Id { get; set; }
  public string? QuestionType { get; set; }
  public string? Content { get; set; } = default!;
  public int Score { get; set; }
}
