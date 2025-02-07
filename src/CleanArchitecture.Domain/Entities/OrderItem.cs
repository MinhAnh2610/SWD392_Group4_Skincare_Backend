namespace CleanArchitecture.Domain.Entities;

public class OrderItem
{
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = default!;
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int Quantity { get; set; }
}
