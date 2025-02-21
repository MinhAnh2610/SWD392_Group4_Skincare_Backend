using CleanArchitecture.Application.DTOs.QuestionOptionDto;

namespace CleanArchitecture.Application.DTOs.QuestionDto;

public class QuestionResponse
{
  public Guid Id { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public string? Instruction { get; set; }
  public string? Section { get; set; }
  public string? QuestionType { get; set; }
  public List<QuestionOptionResponse>? QuestionOptions { get; set; }
}
