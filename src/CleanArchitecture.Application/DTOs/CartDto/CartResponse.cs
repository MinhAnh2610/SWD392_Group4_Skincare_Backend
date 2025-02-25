using CleanArchitecture.Application.DTOs.CartItem;
using CleanArchitecture.Application.DTOs.UserDto;
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

    // Customer info without circular references
    public CustomerDto Customer { get; set; } = default!;

    // Use specialized DTO for cart items to avoid circular references
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
  }
}
