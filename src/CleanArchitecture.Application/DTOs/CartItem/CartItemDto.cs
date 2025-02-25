using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.CartItem
{
  public class CartItemDto
  {
    public Guid CosmeticId { get; set; }
    public string CosmeticName { get; set; } = string.Empty;
    public string CosmeticImage { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Price * Quantity;
  }
}
