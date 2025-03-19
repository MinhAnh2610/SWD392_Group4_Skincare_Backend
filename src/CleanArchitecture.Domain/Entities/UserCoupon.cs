namespace CleanArchitecture.Domain.Entities
{
  public class UserCoupon
  {
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid CouponId { get; set; }
    public Coupon? Coupon { get; set; }
    public int Quantity { get; set; } = 1;
  }
}