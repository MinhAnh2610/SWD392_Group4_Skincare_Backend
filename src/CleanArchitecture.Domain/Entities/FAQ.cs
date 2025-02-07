namespace CleanArchitecture.Domain.Entities;

public class FAQ : Entity<Guid>
{
  public string Question { get; set; } = default!;
  public string Answer { get; set; } = default!;
}
