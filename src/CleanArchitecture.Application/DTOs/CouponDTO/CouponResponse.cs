namespace CleanArchitecture.Application.DTOs.CouponDTO
{
  public class CouponResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public double Discount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int UsageLimit { get; set; }
  }
}
