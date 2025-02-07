namespace CleanArchitecture.Domain.Entities;

public class Order : Entity<Guid>
{
  public Guid CustomerId { get; set; }
  public Decimal SubTotal { get; set; }
  public Decimal TotalPrice { get; set; }
  public DateTime OrderDate { get; set; }
  public string ShippingAddress { get; set; } = default!;
  public string BillingAddress { get; set; } = default!;
  public string TrackingNumber { get; set; } = default!;
  public DateTime DeliveryDate { get; set; }
  public string Status { get; set; } = default!;
  public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
