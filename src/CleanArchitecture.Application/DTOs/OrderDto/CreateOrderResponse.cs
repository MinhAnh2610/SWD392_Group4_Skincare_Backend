using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.OrderDto
{
  public class CreateOrderResponse

  {
    public Guid OrderId { get; set; }
    public string PaymentUrl{ get; set; }
    public string Status { get; set; }
  }
}
