namespace CleanArchitecture.Domain.Entities;

public class QuizAnswer : Entity<Guid>
{
  public Guid QuizResultId { get; set; }
  public QuizResult QuizResult { get; set; } = default!;
  public Guid QuestionId { get; set; }
  public Question Question { get; set; } = default!;
  public string SelectedOptions { get; set; } = default!;
}
