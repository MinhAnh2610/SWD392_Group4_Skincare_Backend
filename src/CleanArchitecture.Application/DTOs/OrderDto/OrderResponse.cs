namespace CleanArchitecture.Application.DTOs.Order
{
  public class OrderResponse
  {
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? CouponId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string BillingAddress { get; set; } = default!;
    public string? TrackingNumber { get; set; } = default!;
    public DateTime DeliveryDate { get; set; }
    public string Status { get; set; } = default!;
    // Audit properties
    public DateTime? CreateAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
  }
}
