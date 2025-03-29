namespace CleanArchitecture.Application.DTOs.CouponDTO
{
  public record GamePointResponse(decimal UserPoints)
  {
    public decimal UserPoints { get; set; } = UserPoints;
  }
}