namespace CleanArchitecture.Domain.Entities;

public class Routine : Entity<Guid>
{
  public Guid SkinTypeId { get; set; }
  public SkinType SkinType { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string Period { get; set; } = default!;
  public List<RoutineStep> RoutineSteps { get; set; } = new List<RoutineStep>();
} 