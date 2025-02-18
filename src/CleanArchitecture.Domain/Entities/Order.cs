namespace CleanArchitecture.Domain.Entities;

public class Order : Entity<Guid>
{
    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = default!;
    public Guid? CouponId { get; set; }
    public Coupon Coupon { get; set; } = default!;
    public Decimal SubTotal { get; set; }
    public Decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string BillingAddress { get; set; } = default!;
    public string TrackingNumber { get; set; } = default!;
    public DateTime DeliveryDate { get; set; }
    public string Status { get; set; } = default!;
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public List<Payment> Payments { get; set; } = new List<Payment>();
    public List<Refund> Refunds { get; set; } = new List<Refund>();
}
