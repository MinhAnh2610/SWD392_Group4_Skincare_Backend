using CleanArchitecture.Application.DTOs.CouponDTO;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

public class CouponService : ICouponService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IValidator<UpdateCouponRequest> _updateCouponValidator;
  private readonly IValidator<CreateCouponRequest> _createCouponValidator;

  public CouponService(
      IUnitOfWork unitOfWork,
      IErrorFactory errorFactory,
      IValidator<UpdateCouponRequest> updateValidator,
      IValidator<CreateCouponRequest> createCouponValidator)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _updateCouponValidator = updateValidator;
    _createCouponValidator = createCouponValidator;
  }

  private CouponResponse MapToCouponResponse(Coupon coupon)
  {
    return new CouponResponse
    {
      Id = coupon.Id,
      Name = coupon.Name,
      Code = coupon.Code,
      Discount = coupon.DiscountAmount,
      StartDate = coupon.StartDate,
      ExpiryDate = coupon.EndDate,
      UsageLimit = coupon.UsageLimit,
      MaxDiscountAmount = coupon.MaxDiscountAmount,
      MinimumOrderPrice = coupon.MinimumOrderPrice
    };
  }

  public async Task<Result<List<CouponResponse>>> GetAllCoupons()
  {
    try
    {
      var coupons = await _unitOfWork.Coupons.GetAllAsync();
      var response = coupons.Select(MapToCouponResponse).ToList();
      return Result<List<CouponResponse>>.Success(response, StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<List<CouponResponse>>.Failure(
          [new Error("Coupon.GetAll", ex.Message)],
          StatusCodes.Status500InternalServerError);
    }
  }

  public async Task<Result<CouponResponse>> GetCouponById(Guid id)
  {
    var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
    if (coupon == null)
    {
      return Result<CouponResponse>.Failure(
          [new Error("Coupon.Get", "Coupon not found")],
          StatusCodes.Status404NotFound);
    }

    return Result<CouponResponse>.Success(MapToCouponResponse(coupon), StatusCodes.Status200OK);
  }

  public async Task<Result<CouponResponse>> GetCouponByCode(string code)
  {
    var coupons = await _unitOfWork.Coupons.GetAllAsync();
    var coupon = coupons.FirstOrDefault(c => c.Code == code);

    if (coupon == null)
    {
      return Result<CouponResponse>.Failure(
          [new Error("Coupon.Get", "Coupon not found")],
          StatusCodes.Status404NotFound);
    }

    if (coupon.UsageLimit <= 0)
    {
      return Result<CouponResponse>.Failure(
          [new Error("Coupon.Get", "Coupon usage limit exceeded")],
          StatusCodes.Status400BadRequest);
    }

    if (coupon.EndDate < DateTime.Now)
    {
      return Result<CouponResponse>.Failure(
          [new Error("Coupon.Get", "Coupon has expired")],
          StatusCodes.Status400BadRequest);
    }

    return Result<CouponResponse>.Success(MapToCouponResponse(coupon), StatusCodes.Status200OK);
  }

  public async Task<Result<CouponResponse>> CreateCoupon(CreateCouponRequest request)
  {
    var validationResult = await _createCouponValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var errors = _errorFactory.CreateValidationError("Coupon", validationResult);
      return Result<CouponResponse>.Failure(errors.errs, errors.statusCode);
    }

    var coupon = new Coupon
    {
      Name = request.Name,
      Code = request.Code,
      DiscountAmount = request.Discount,
      StartDate = DateTime.Now,
      EndDate = request.ExpiryDate,
      UsageLimit = request.UsageLimit,
      IsActive = true,
      MaxDiscountAmount = request.MaxDiscountAmount,
      MinimumOrderPrice = request.MinimumOrderPrice
    };

    await _unitOfWork.Coupons.CreateAsync(coupon);
    var isSaved = await _unitOfWork.CompleteAsync();

    if (!isSaved)
    {
      var error = _errorFactory.CreateDatabaseError("Coupon");
      return Result<CouponResponse>.Failure([error.err], error.statusCode);
    }

    var response = MapToCouponResponse(coupon);
    return Result<CouponResponse>.Success(response, StatusCodes.Status201Created);
  }

  public async Task<Result<CouponResponse>> UpdateCoupon(UpdateCouponRequest request, Guid id)
  {
    var validationResult = await _updateCouponValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var errors = _errorFactory.CreateValidationError("Coupon", validationResult);
      return Result<CouponResponse>.Failure(errors.errs, errors.statusCode);
    }

    var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
    if (coupon == null)
    {
      return Result<CouponResponse>.Failure(
          [new Error("Coupon.Update", "Coupon not found")],
          StatusCodes.Status404NotFound);
    }

    coupon.Code = request.Code;
    coupon.DiscountAmount = request.Discount;
    coupon.EndDate = request.ExpiryDate;
    coupon.UsageLimit = request.UsageLimit;
    coupon.MaxDiscountAmount = request.MaxDiscountAmount ?? coupon.MaxDiscountAmount;
    coupon.MinimumOrderPrice = request.MinimumOrderPrice ?? coupon.MinimumOrderPrice;

    _unitOfWork.Coupons.Update(coupon);
    var isSaved = await _unitOfWork.CompleteAsync();

    if (!isSaved)
    {
      var error = _errorFactory.CreateDatabaseError("Coupon");
      return Result<CouponResponse>.Failure([error.err], error.statusCode);
    }

    return Result<CouponResponse>.Success(MapToCouponResponse(coupon), StatusCodes.Status200OK);
  }

  public async Task<Result<CouponResponse>> DeleteCoupon(Guid id)
  {
    var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
    if (coupon == null)
    {
      return Result<CouponResponse>.Failure(
          [new Error("Coupon.Delete", "Coupon not found")],
          StatusCodes.Status404NotFound);
    }

    _unitOfWork.Coupons.Remove(coupon);
    var isSaved = await _unitOfWork.CompleteAsync();

    if (!isSaved)
    {
      var error = _errorFactory.CreateDatabaseError("Coupon");
      return Result<CouponResponse>.Failure([error.err], error.statusCode);
    }

    return Result<CouponResponse>.Success(MapToCouponResponse(coupon), StatusCodes.Status200OK);
  }

 
}