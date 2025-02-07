namespace CleanArchitecture.Domain.Entities;

public class Feedback : Entity<Guid>
{
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public string? Content { get; set; }
  public decimal Rating { get; set; }
}