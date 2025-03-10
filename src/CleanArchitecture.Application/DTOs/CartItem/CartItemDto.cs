namespace CleanArchitecture.Application.DTOs.CartItem
{
  public class CartItemDto
  {
    public Guid CosmeticId { get; set; }
    public string CosmeticName { get; set; } = string.Empty;
    public string CosmeticImage { get; set; } = string.Empty;
    public decimal Price { get; set; } // Original price
    public decimal DiscountedPrice { get; set; } // Price after event discount
    public decimal DiscountPercentage { get; set; } // Event discount percentage
    public int Quantity { get; set; }
    public decimal Subtotal => Price * Quantity; // Original subtotal
    public decimal DiscountedSubtotal => DiscountedPrice * Quantity; // Subtotal after event discount
    public int Weight { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
  }
}