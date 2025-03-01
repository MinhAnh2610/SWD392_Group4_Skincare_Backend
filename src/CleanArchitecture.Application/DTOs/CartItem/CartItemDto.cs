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
    public int Weight { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
  }
}
