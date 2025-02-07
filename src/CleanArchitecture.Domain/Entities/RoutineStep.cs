namespace CleanArchitecture.Domain.Entities;

public class RoutineStep : Entity<Guid>
{
  public Guid RoutineId { get; set; }
  public Routine Routine { get; set; } = default!;
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int StepNumber { get; set; }
}