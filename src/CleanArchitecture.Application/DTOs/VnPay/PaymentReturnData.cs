using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.VnPay
{
  public class PaymentReturnData
  {
    public string TransactionId { get; set; } = default!;
    public string? TotalAmount { get; set; }
    public string? ResponseCode { get; set; }
  }
}
