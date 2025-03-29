namespace CleanArchitecture.Domain.Entities;

public class QuizResult : Entity<Guid>
{
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public Guid QuizId { get; set; }
  public Quiz Quiz { get; set; } = default!;
  public Guid SkinTypeId { get; set; }
  public SkinType SkinType { get; set; } = default!;
  public List<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
}
