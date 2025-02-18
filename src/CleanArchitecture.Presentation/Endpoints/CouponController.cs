
using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.DTOs.CouponDTO;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class CouponController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/coupon").WithTags("Coupon Management");
      #region Apply Coupon API
      group.MapGet("/apply-coupon", async (ICouponService couponService, ApplyCouponRequest applyCouponRequest) =>
      {
        var result = await couponService.ApplyCoupon(applyCouponRequest);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(result.Data!, "Apply Coupon Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("ApplyCoupon")
      .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("ApplyCoupon")
      .WithDescription("Apply Coupon")
      .RequireAuthorization();
      #endregion
    }
  }
}
