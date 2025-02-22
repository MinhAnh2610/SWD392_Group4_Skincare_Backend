using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cart
{
  public class AddProductRequest
  {
    public Guid CartId { get; set; }
    public Guid CosmeticId { get; set; }
    public int Quantity { get; set; }
  }
}
