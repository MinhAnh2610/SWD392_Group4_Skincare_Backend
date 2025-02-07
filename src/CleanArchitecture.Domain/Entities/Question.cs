namespace CleanArchitecture.Domain.Entities;

public class Question : Entity<Guid>
{
  public Guid QuizId { get; set; }
  public Quiz Quiz { get; set; } = default!;
  public string? Title { get; set; }
  public string? Description { get; set; }
  public string? Instruction { get; set; }
  public string? Section { get; set; }
  public List<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
}