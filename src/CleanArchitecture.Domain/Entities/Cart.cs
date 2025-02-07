namespace CleanArchitecture.Domain.Entities;

public class Cart : Entity<Guid>
{
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public decimal TotalPrice { get; set; }
  public List<CartItems> CartItems { get; set; } = new List<CartItems>();
}
