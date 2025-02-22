namespace CleanArchitecture.Domain.Entities;

public class QuestionOption : Entity<Guid>
{
  public Guid QuestionId { get; set; }
  public Question? Question { get; set; }
  public string? Content { get; set; } = default!;
  public int Score { get; set; }
}