namespace CleanArchitecture.Application.DTOs.RefundItem;

public class RefundItemResponse
{
  public Guid Id { get; set; }
  public int Quantity { get; set; }
  public string? Reason { get; set; }
}
