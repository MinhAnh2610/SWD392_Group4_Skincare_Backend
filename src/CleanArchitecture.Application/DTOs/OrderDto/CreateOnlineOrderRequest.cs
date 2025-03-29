namespace CleanArchitecture.Application.DTOs.OrderDto;

public class CreateOnlineOrderRequest : ICreateOrderRequest
{
    public Guid? CouponId { get; set; }
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public string Currency  { get; set; }
    public string WardCode { get; set; }
    public int DistrictId { get; set; }
}
