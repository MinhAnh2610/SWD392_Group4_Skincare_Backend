namespace CleanArchitecture.Domain.Entities;

public class Testimonial : Entity<Guid>
{
  public Guid CustomerId { get; set; }
  public string Content { get; set; } = default!;
  public int Rating { get; set; }
}
