namespace CleanArchitecture.Application.DTOs.OrderItemDto;

public class OrderItemResponse
{
  public Guid CosmeticId { get; set; }
  public int Quantity { get; set; }
  public decimal SellingPrice { get; set; }
  public decimal SubTotal => SellingPrice * Quantity;
}
