namespace CleanArchitecture.Domain.Entities;

public class QuestionOption : Entity<Guid>
{
  public Guid QuestionId { get; set; }
  public Question? Question { get; set; }
  public Guid QuestionTypeId { get; set; }
  public QuestionType? QuestionType { get; set; }
  public string? Content { get; set; } = default!;
}