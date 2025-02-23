using CleanArchitecture.Application.DTOs.CouponDTO;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class CouponService : ICouponService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IErrorFactory _errorFactory; 

    private readonly IValidator<ApplyCouponRequest> _couponValidator;

    public CouponService(IUnitOfWork unitOfWork, IValidator<ApplyCouponRequest> couponValidator, IErrorFactory errorFactory)
    {
      _unitOfWork = unitOfWork;
      _couponValidator = couponValidator;
      _errorFactory = errorFactory;
    }

    public async Task<Result<CouponResponse>> ApplyCoupon(ApplyCouponRequest applyCouponRequest)
    {
      var validationResult = await _couponValidator.ValidateAsync(applyCouponRequest);
      if (!validationResult.IsValid)
      {
        var errors = validationResult.Errors
          .Select(e => new Error("ValidationError", e.ErrorMessage))
          .ToList();

        return Result<CouponResponse>.Failure(errors, StatusCodes.Status400BadRequest);
      }

      var coupon = await _unitOfWork.Coupons.GetByIdAsync(applyCouponRequest.code);
      if (coupon == null)
      {
        return Result<CouponResponse>.Failure(
               new List<Error> { new Error("Coupon.Apply", "Coupon Not Found") },
               StatusCodes.Status404NotFound
               );
      }
      
      var order = await _unitOfWork.Orders.GetByIdAsync(applyCouponRequest.order);

      if (order == null)
      {
        return Result<CouponResponse>.Failure(
               new List<Error> { new Error("Coupon.Apply", "Order Not Found") },
               StatusCodes.Status404NotFound
               );
      }

      if (coupon.EndDate < DateTime.Now)
      {
        return Result<CouponResponse>.Failure(
               new List<Error> { new Error("Coupon.Apply", "Coupon Expired") },
               StatusCodes.Status400BadRequest
               );
      }

      if (coupon.StartDate > DateTime.Now)
      {
        return Result<CouponResponse>.Failure(
               new List<Error> { new Error("Coupon.Apply", "Coupon Not Started") },
               StatusCodes.Status400BadRequest
               );
      }

      if(coupon.UsageLimit <= 0)
      {
        return Result<CouponResponse>.Failure(
               new List<Error> { new Error("Coupon.Apply", "Coupon Limit Exceeded") },
               StatusCodes.Status400BadRequest
               );
      }

      order.Coupon = coupon;
      _unitOfWork.Orders.Update(order);
      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Coupon");
        return Result<CouponResponse>.Failure([error.err], error.statusCode);
      }
      return Result<CouponResponse>.Success(new CouponResponse
      {
        Id = coupon.Id,
        Code = coupon.Code,
        Discount = coupon.DiscountAmount,
        ExpiryDate = coupon.EndDate,
        UsageLimit = coupon.UsageLimit
      }, StatusCodes.Status200OK);
    }
  }
}
