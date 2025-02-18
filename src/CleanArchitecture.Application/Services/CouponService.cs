using CleanArchitecture.Application.DTOs.CouponDTO;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
  public class CouponService : ICouponService
  {
    private readonly ICouponRepository _couponRepository;

    private readonly IOrderRepository _orderRepository;

    private readonly IValidator<ApplyCouponRequest> _couponValidator;

    public CouponService(ICouponRepository couponRepository, IValidator<ApplyCouponRequest> couponValidator, IOrderRepository orderRepository)
    {
      _couponRepository = couponRepository;
      _couponValidator = couponValidator;
      _orderRepository = orderRepository;
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

      var coupon = await _couponRepository.GetByIdAsync(applyCouponRequest.code);
      if (coupon == null)
      {
        return Result<CouponResponse>.Failure(
               new List<Error> { new Error("Coupon.Apply", "Coupon Not Found") },
               StatusCodes.Status404NotFound
               );
      }
      
      var order = await _orderRepository.GetByIdAsync(applyCouponRequest.order);

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
      await _orderRepository.UpdateAsync(order);
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
