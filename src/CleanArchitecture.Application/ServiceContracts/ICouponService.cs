using CleanArchitecture.Application.DTOs.CouponDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface ICouponService
  { 

    Task<Result<CouponResponse>> CreateCoupon(CreateCouponRequest createCouponRequest); 
    Task<Result<CouponResponse>> UpdateCoupon(UpdateCouponRequest updateCouponRequest, Guid id);
    Task<Result<CouponResponse>> DeleteCoupon(Guid id);
    Task<Result<List<CouponResponse>>> GetAllCoupons();
    Task<Result<CouponResponse>> GetCouponById(Guid id);
    Task<Result<CouponResponse>> GetCouponByCode(string code);
    Task<Result<CouponResponse>> ExchangeCouponAsync(ExchangeCouponRequest request);
    Task<Result<GamePointResponse>> ProcessGamePoints(GamePointRequest request);
    Task<Result<string>> StartGameAsync();
  }
}
