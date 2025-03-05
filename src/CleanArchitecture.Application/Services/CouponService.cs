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

      if (coupon.UsageLimit <= 0)
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

    public async Task<Result<CouponResponse>> CreateCoupon(CreateCouponRequest createCouponRequest)
    {
      var coupon = new Coupon
      {
        Code = createCouponRequest.Code,
        DiscountAmount = createCouponRequest.Discount,
        StartDate = DateTime.Now,
        EndDate = createCouponRequest.ExpiryDate,
        UsageLimit = createCouponRequest.UsageLimit
      };

      await _unitOfWork.Coupons.CreateAsync(coupon);
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
      }, StatusCodes.Status201Created);
    }

    public async Task<Result<CouponResponse>> DeleteCoupon(Guid id)
    {
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
      if (coupon == null)
      {
        return Result<CouponResponse>.Failure(
                 new List<Error> { new Error("Coupon.Delete", "Coupon Not Found") },
                        StatusCodes.Status404NotFound
                               );
      }

      _unitOfWork.Coupons.Remove(coupon);
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

    public async Task<Result<List<CouponResponse>>> GetAllCoupons()
    {
      var coupons = await _unitOfWork.Coupons.GetAllAsync();
      return Result<List<CouponResponse>>.Success(coupons.Select(c => new CouponResponse
      {
        Id = c.Id,
        Code = c.Code,
        Discount = c.DiscountAmount,
        ExpiryDate = c.EndDate,
        UsageLimit = c.UsageLimit
      }).ToList(), StatusCodes.Status200OK);
    }

    public async Task<Result<CouponResponse>> UpdateCoupon(UpdateCouponRequest updateCouponRequest)
    {
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(updateCouponRequest.Id);
      if (coupon == null)
      {
        return Result<CouponResponse>.Failure(
                 new List<Error> { new Error("Coupon.Update", "Coupon Not Found") },
                        StatusCodes.Status404NotFound
                               );
      }

      coupon.Code = updateCouponRequest.Code;
      coupon.DiscountAmount = updateCouponRequest.Discount;
      coupon.EndDate = updateCouponRequest.ExpiryDate;
      coupon.UsageLimit = updateCouponRequest.UsageLimit;

      _unitOfWork.Coupons.Update(coupon);
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

    public async Task<Result<CouponResponse>> GetCouponById(Guid id)
    {
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
      if (coupon == null)
      {
        return Result<CouponResponse>.Failure(
        new List<Error> { new Error("Coupon.Get", "Coupon Not Found") },
        StatusCodes.Status404NotFound
        );
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


    //Just in case API if error occurs remove this method
    public async Task<Result<CouponResponse>> RemoveCoupon(string code, Guid orderId)
    {
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(code);
      if (coupon == null)
      {
        return Result<CouponResponse>.Failure(
        new List<Error> { new Error("Coupon.Remove", "Coupon Not Found") },
        StatusCodes.Status404NotFound
        );
      }

      var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
      if (order == null)
      {
        return Result<CouponResponse>.Failure(
        new List<Error> { new Error("Coupon.Remove", "Order Not Found") },
        StatusCodes.Status404NotFound
        );
      }

      if (order.Coupon == null)
      {
        return Result<CouponResponse>.Failure(
                        new List<Error> { new Error("Coupon.Remove", "Coupon Not Applied") },
                                      StatusCodes.Status400BadRequest
                                                    );
      }

      if (order.Coupon.Code != code)
      {
        return Result<CouponResponse>.Failure(
                        new List<Error> { new Error("Coupon.Remove", "Coupon Not Applied") },
                                      StatusCodes.Status400BadRequest
                                                    );
      }

      order.Coupon = null;
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
