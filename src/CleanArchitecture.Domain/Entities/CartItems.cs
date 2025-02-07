namespace CleanArchitecture.Domain.Entities;

public class CartItems
{
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int Quantity { get; set; }
}
