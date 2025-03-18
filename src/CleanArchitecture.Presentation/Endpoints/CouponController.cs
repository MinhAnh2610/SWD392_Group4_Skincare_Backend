using CleanArchitecture.Application.DTOs.CouponDTO;
using Microsoft.AspNetCore.Mvc;

public class CouponController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/coupons").WithTags("Coupon Management");

    group.MapGet("/", async (ICouponService couponService) =>
    {
      var result = await couponService.GetAllCoupons();
      return result.Match("Retrieved all coupons successfully.");
    })
    .WithName("GetAllCoupons")
    .Produces<ApiResponse<List<CouponResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .RequireAuthorization();

    group.MapGet("/{id:guid}", async (ICouponService couponService, Guid id) =>
    {
      var result = await couponService.GetCouponById(id);
      return result.Match("Retrieved coupon successfully.");
    })
    .WithName("GetCouponById")
    .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .RequireAuthorization();

    group.MapGet("/code/{code}", async (ICouponService couponService, string code) =>
    {
      var result = await couponService.GetCouponByCode(code);
      return result.Match("Retrieved coupon successfully.");
    })
    .WithName("GetCouponByCode")
    .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .RequireAuthorization();

    group.MapPost("/", async (ICouponService couponService, [FromBody] CreateCouponRequest request) =>
    {
      var result = await couponService.CreateCoupon(request);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(
            result.Data!,
            "Coupon created successfully"));
      }

      return Results.BadRequest(ApiResponse<CouponResponse>.FailureResponse(
          result.Errors,
          "Failed to create coupon"));
    })
        .WithName("CreateCoupon")
        .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

    group.MapPut("/{id:guid}", async (ICouponService couponService, Guid id, [FromBody] UpdateCouponRequest request) =>
    {
      // Ensure the route ID matches the request ID


      var result = await couponService.UpdateCoupon(request, id);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(
            result.Data!,
            "Coupon updated successfully"));
      }

      return Results.BadRequest(ApiResponse<CouponResponse>.FailureResponse(
          result.Errors,
          "Failed to update coupon"));
    })
.WithName("UpdateCoupon")
.Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.RequireAuthorization();

    group.MapDelete("/{id:guid}", async (ICouponService couponService, Guid id) =>
    {
      var result = await couponService.DeleteCoupon(id);
      return result.Match("Coupon deleted successfully.");
    })
    .WithName("DeleteCoupon")
    .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .RequireAuthorization();
  }
}