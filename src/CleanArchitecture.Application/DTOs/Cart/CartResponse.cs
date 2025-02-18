using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cart
{
  public class CartResponse
  {
      public Guid Id { get; set; }
      public decimal TotalPrice { get; set; }

      public CleanArchitecture.Domain.Entities.User Customer { get; set; } = default!;

      public List<CleanArchitecture.Domain.Entities.CartItem>? Items { get; set; } = new List<CleanArchitecture.Domain.Entities.CartItem>();

  }
}
