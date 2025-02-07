namespace CleanArchitecture.Domain.Entities;

public class QuestionOption : Entity<Guid>
{
  public Guid QuestionId { get; set; }
  public Guid QuestionTypeId { get; set; }
  public QuestionType QuestionType { get; set; } = default!;
  public string Content { get; set; } = default!;
  public List<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
}