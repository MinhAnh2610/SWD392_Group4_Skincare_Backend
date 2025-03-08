namespace CleanArchitecture.Application.DTOs.OrderDto
{
  public interface ICreateOrderRequest
  {
    string PaymentMethod { get; set; }
    Guid? CouponId { get; set; }
  }
}