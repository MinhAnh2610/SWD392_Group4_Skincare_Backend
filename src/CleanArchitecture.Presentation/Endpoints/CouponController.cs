using CleanArchitecture.Application.DTOs.CouponDTO;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class CouponController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/coupon").WithTags("Coupon Management");
      #region Apply Coupon API
      group.MapGet("/apply-coupon", async (ICouponService couponService, [FromBody] ApplyCouponRequest applyCouponRequest) =>
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
      .Produces<ApiResponse<List<CouponResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("ApplyCoupon")
      .WithDescription("Apply Coupon")
      .RequireAuthorization();
      #endregion

      #region Create Coupon API
      group.MapPost("/create-coupon", async (ICouponService couponService, [FromBody] CreateCouponRequest createCouponRequest) =>
      {
        var result = await couponService.CreateCoupon(createCouponRequest);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(result.Data!, "Create Coupon Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
        .WithName("CreateCoupon")
        .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("CreateCoupon")
        .WithDescription("Create Coupon")
        .RequireAuthorization();
      #endregion

      #region Update Coupon API
      group.MapPut("/update-coupon", async (ICouponService couponService, [FromBody] UpdateCouponRequest updateCouponRequest) =>
      {
        var result = await couponService.UpdateCoupon(updateCouponRequest);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(result.Data!, "Update Coupon Successfully."));
        }
        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("UpdateCoupon")
      .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("UpdateCoupon")
      .WithDescription("Update Coupon")
      .RequireAuthorization();
      #endregion

      #region Delete Coupon API
      group.MapDelete("/delete-coupon/{id}", async (ICouponService couponService, Guid id) =>
      {
        var result = await couponService.DeleteCoupon(id);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(result.Data!, "Delete Coupon Successfully."));
        }
        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
        .WithName("DeleteCoupon")
        .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("DeleteCoupon")
        .WithDescription("Delete Coupon")
        .RequireAuthorization();
      #endregion

      #region Get All Coupons API
      group.MapGet("/get-all-coupons", async (ICouponService couponService) =>
      {
        var result = await couponService.GetAllCoupons();
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<CouponResponse>>.SuccessResponse(result.Data!, "Get All Coupons Successfully."));
        }
        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
        .WithName("GetAllCoupons")
        .Produces<ApiResponse<List<CouponResponse>>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("GetAllCoupons")
        .WithDescription("Get All Coupons")
        .RequireAuthorization();
      #endregion

      #region Get Coupon By Id API
      group.MapGet("/get-coupon-by-id/{id}", async (ICouponService couponService, Guid id) =>
      {
        var result = await couponService.GetCouponById(id);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(result.Data!, "Get Coupon By Id Successfully."));
        }
        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
        .WithName("GetCouponById")
        .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("GetCouponById")
        .WithDescription("Get Coupon By Id")
        .RequireAuthorization();
      #endregion

      #region Get Coupon By Code API
      group.MapGet("/get-coupon-by-code/{code}", async (ICouponService couponService, string code) =>
      {
        var result = await couponService.GetCouponByCode(code);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CouponResponse>.SuccessResponse(result.Data!, "Get Coupon By Code Successfully."));
        }
        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
        .WithName("GetCouponByCode")
        .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("GetCouponByCode")
        .WithDescription("Get Coupon By Code")
        .RequireAuthorization();
      #endregion
    }
  }
}
