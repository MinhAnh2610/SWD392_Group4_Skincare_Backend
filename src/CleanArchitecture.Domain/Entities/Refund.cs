namespace CleanArchitecture.Domain.Entities;

public class Refund : Entity<Guid>
{
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = default!;
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public Guid? StaffId { get; set; }
  public User? Staff { get; set; } = default!;
  public string Reason { get; set; } = default!;
  public decimal TotalAmount { get; set; }
  public string Status { get; set; } = default!;
  public string Method { get; set; } = default!;
  public List<RefundItem> RefundItems { get; set; } = new List<RefundItem>();
}
