using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.CouponDTO
{
  public record ApplyCouponRequest(string code, Guid order);
}
