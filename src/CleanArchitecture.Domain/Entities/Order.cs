public class Order : Entity<Guid>
{
  public Guid CustomerId { get; set; }
  public User Customer { get; set; } = default!;
  public Guid? CouponId { get; set; }
  public Coupon? Coupon { get; set; }  // made nullable if coupon is optional
  public decimal SubTotal { get; set; }
  public decimal TotalPrice { get; set; }
  public DateTime OrderDate { get; set; }
  public string ShippingAddress { get; set; } = default!;
  public string BillingAddress { get; set; } = default!;
  public string? TrackingNumber { get; set; }
  public DateTime? DeliveryDate { get; set; } // made nullable if delivery date is set later
  public string Status { get; set; } = default!;
  public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  public List<Refund> Refunds { get; set; } = new List<Refund>();
}
