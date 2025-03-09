using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.CouponDTO
{
  public class CreateCouponRequest
  {
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public double Discount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int UsageLimit { get; set; }
  }
}
