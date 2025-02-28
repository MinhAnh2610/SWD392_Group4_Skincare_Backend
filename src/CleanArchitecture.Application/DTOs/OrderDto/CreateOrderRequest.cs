using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.OrderDto
{
  public class CreateOrderRequest
  {
    public Guid CartId { get; set; }
    public Guid? CouponId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string BillingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty; // "VNPAY", "CARD", etc.
    public string Currency { get; set; } = string.Empty; // "VND", "USD"
    public decimal Amount { get; set; }
  }
}
