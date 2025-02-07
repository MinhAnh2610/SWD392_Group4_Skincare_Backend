namespace CleanArchitecture.Domain.Entities;

public class OrderItem
{
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int Quantity { get; set; }
}
