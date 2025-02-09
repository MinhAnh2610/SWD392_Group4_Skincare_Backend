namespace CleanArchitecture.Domain.Entities;

public class Cart : Entity<Guid>
{
  public User Customer { get; set; } = default!;
  public decimal TotalPrice { get; set; }
  public List<CartItem>? CartItems { get; set; } = new List<CartItem>();
}
