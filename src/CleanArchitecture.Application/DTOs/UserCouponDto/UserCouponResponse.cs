using CleanArchitecture.Application.DTOs.CouponDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.UserCouponDto
{
  public class UserCouponResponse
  {
    public Guid UserId { get; set; }
    public Guid CouponId { get; set; }
    public CouponResponse Coupon { get; set; } = default!;
    public int Quantity { get; set; }
  }
}
