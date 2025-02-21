using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Order
{
  public class CheckOutRequest
  {
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public Guid? CouponId { get; set; }
    public string BillingAddress { get; set; }  
    public string ShippingAddress { get; set; } 

  }
}
