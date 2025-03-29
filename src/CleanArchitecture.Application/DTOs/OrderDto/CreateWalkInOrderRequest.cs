namespace CleanArchitecture.Application.DTOs.OrderDto
{
  public class CreateWalkInOrderRequest : ICreateOrderRequest
  {
    public Dictionary<Guid, int> Cosmetics { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CustomerPhoneNumber { get; set; }
    public Guid? CouponId { get; set; }
    public string? PaymentMethod { get; set; }
  }
}