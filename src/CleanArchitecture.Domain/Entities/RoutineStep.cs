using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class RoutineStep : Entity<Guid>
{
    public Guid RoutineId { get; set; }
    public Guid StepId { get; set; }
    public int StepNumber { get; set; }
}