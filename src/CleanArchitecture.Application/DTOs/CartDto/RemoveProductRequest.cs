using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cart
{
  public class RemoveProductRequest
  {
    public Guid CartId { get; set; }
    public Guid CosmeticId { get; set; }
  }
}
