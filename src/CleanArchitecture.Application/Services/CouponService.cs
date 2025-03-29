using CleanArchitecture.Application.DTOs.CouponDTO;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

public class CouponService : ICouponService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IValidator<UpdateCouponRequest> _updateCouponValidator;
  private readonly ITimeZoneService _timeZoneService;
  private readonly IValidator<CreateCouponRequest> _createCouponValidator;
  private readonly IClaimsService _claimsService;
  private const int LIMIT_PLAY_TIMES = 3;
  private const string LIMIT_DATE_TYPE = "month";

  public CouponService(
      IUnitOfWork unitOfWork,
      IErrorFactory errorFactory,
      IValidator<UpdateCouponRequest> updateValidator,
      IValidator<CreateCouponRequest> createCouponValidator, IClaimsService claimsService, ITimeZoneService timeZoneService)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _updateCouponValidator = updateValidator;
    _createCouponValidator = createCouponValidator;
    _claimsService = claimsService;
    _timeZoneService = timeZoneService;
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
      MinimumOrderPrice = coupon.MinimumOrderPrice,
      PointRequired = coupon.PointRequired
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

  public async Task<Result<CouponResponse>> ExchangeCouponAsync(ExchangeCouponRequest request)
  {
    var user = await _unitOfWork.Users.GetByIdAsync(_claimsService.CurrentUserId);
    if (user is null)
    { 
      var error = _errorFactory.CreateNotFoundError("User");
      return Result<CouponResponse>.Failure([error.err], error.statusCode);
    }
    
    var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.CouponId);
    if (coupon is null)
    { 
      var error = _errorFactory.CreateNotFoundError("Coupon");
      return Result<CouponResponse>.Failure([error.err], error.statusCode);
    }

    if (user.Point < coupon.PointRequired)
      return Result<CouponResponse>.Failure([new Error("Coupon.Exchange", "User doesn't have enough points to exchange.")],StatusCodes.Status400BadRequest);
    
    var userCoupon = await _unitOfWork.UserCoupons.GetByIdAsync(user.Id, coupon.Id);
    if (userCoupon is null)
    {
      userCoupon = new UserCoupon() { UserId = user.Id, CouponId = coupon.Id, Quantity = 1 };
      _unitOfWork.UserCoupons.Create(userCoupon);
    }
    else
      userCoupon.Quantity++;

    user.Point -= coupon.PointRequired;
    
    await _unitOfWork.CompleteAsync();
    return Result<CouponResponse>.Success(MapToCouponResponse(coupon), StatusCodes.Status200OK);
  }

  public async Task<Result<GamePointResponse>> ProcessGamePoints(GamePointRequest request)
  {
    var user = await _unitOfWork.Users.GetByIdAsync(_claimsService.CurrentUserId);
    if (user is null)
    {
      var error = _errorFactory.CreateNotFoundError("User");
      return Result<GamePointResponse>.Failure([error.err], error.statusCode);
    }
    
    var userPoints = ProcessGamePoints(request.Points);
    
    user.Point += userPoints;
    
    // Log play attempt
    
    _unitOfWork.PlayLogs.Create(new PlayLog()
    {
      UserId = user.Id,
      PlayTimeStamp = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      IsActive = true,
      CreateAt =  _timeZoneService.ConvertToLocalTime(DateTime.UtcNow),
      CreatedBy = user.UserName
    });
    await _unitOfWork.CompleteAsync();
      
    GamePointResponse gamePointResponse = new(userPoints);
    return Result<GamePointResponse>.Success(gamePointResponse, StatusCodes.Status200OK);
  }

  public async Task<Result<string>> StartGameAsync()
  {
    var user = await _unitOfWork.Users.GetByIdAsync(_claimsService.CurrentUserId);
    if (user is null)
    {
      var error = _errorFactory.CreateNotFoundError("User");
      return Result<string>.Failure([error.err], error.statusCode);
    }

    var timesPlayed = await _unitOfWork.PlayLogs.GetPlayTimesAsync(LIMIT_DATE_TYPE, user);
    if (timesPlayed >= LIMIT_PLAY_TIMES)
    {
      return Result<string>.Failure(
        [new Error("CouponGame.ExceedPlayTimes", "User has reached attempt limit this month")],
        StatusCodes.Status400BadRequest);
    }
    
    
    return Result<string>.Success("Game started successfully.", StatusCodes.Status200OK);
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
      MinimumOrderPrice = request.MinimumOrderPrice,
      PointRequired = request.PointRequired
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

  private decimal ProcessGamePoints(int points)
  {
    decimal userPoints = points switch
    {
      _ when points > 100 => 10.0m,  
      _ when points is >= 50 and <= 100 => 5.0m,  
      _ when points is >= 10 and < 50 => 2.0m,  
      _ => 0.0m  
    };
    
    return userPoints;
  }
}