namespace CleanArchitecture.Application.DTOs.RefundItem;

public class CreateRefundItemRequest
{
  public Guid CosmeticId { get; set; }
  public int Quantity { get; set; }
  public string? Reason { get; set; }
}
