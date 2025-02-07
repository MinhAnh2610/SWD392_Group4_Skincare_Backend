namespace CleanArchitecture.Domain.Entities;

public class Policy : Entity<Guid>
{
  public string Title { get; set; } = default!;
  public string Content { get; set; } = default!;
}
