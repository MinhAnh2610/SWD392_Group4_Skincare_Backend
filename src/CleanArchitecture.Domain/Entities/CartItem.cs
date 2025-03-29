namespace CleanArchitecture.Domain.Entities;

public class CartItem
{
  public Guid CartId { get; set; }
  public Cart Cart { get; set; } = default!;
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int Quantity { get; set; }
}
