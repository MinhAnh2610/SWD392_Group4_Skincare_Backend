using CleanArchitecture.Application.DTOs.CartItem;
using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.DTOs.Cart
{
  public class CartResponse
  {
    public Guid Id { get; set; }
    public decimal TotalPrice => Items.Sum(ci => ci.Subtotal);

    // Customer info without circular references
    public CustomerDto Customer { get; set; } = default!;

    // Use specialized DTO for cart items to avoid circular references
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
  }
}
