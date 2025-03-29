namespace CleanArchitecture.Domain.Entities;

public class RefundItem : Entity<Guid>
{
  public Guid RefundId { get; set; }
  public Refund Refund { get; set; } = default!;
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int Quantity { get; set; }
  public string Reason { get; set; } = default!;
}
