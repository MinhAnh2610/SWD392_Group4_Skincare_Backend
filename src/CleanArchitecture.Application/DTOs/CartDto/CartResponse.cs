using CleanArchitecture.Application.DTOs.CartItem;
using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.DTOs.Cart
{
  public class CartResponse
  {
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; } // This will be the discounted total price
    public decimal OriginalTotalPrice { get; set; } // Total before any discounts
    public decimal EventDiscountTotal { get; set; } // Total event discounts

    // Customer info without circular references
    public CustomerDto Customer { get; set; } = default!;

    // Use specialized DTO for cart items to avoid circular references
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
  }
}