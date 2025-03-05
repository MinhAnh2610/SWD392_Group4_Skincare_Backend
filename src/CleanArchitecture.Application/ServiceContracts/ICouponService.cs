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
    Task<Result<CouponResponse>> ApplyCoupon(ApplyCouponRequest applyCouponRequests);

    Task<Result<CouponResponse>> CreateCoupon(CreateCouponRequest createCouponRequest); 
    Task<Result<CouponResponse>> UpdateCoupon(UpdateCouponRequest updateCouponRequest);
    Task<Result<CouponResponse>> DeleteCoupon(Guid id);
    Task<Result<List<CouponResponse>>> GetAllCoupons();
    Task<Result<CouponResponse>> GetCouponById(Guid id);
  }
}
