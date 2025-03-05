using CleanArchitecture.Application.DTOs.RefundItem;

namespace CleanArchitecture.Application.DTOs.Refund;

public class CreateRefundRequest
{
  public Guid OrderId { get; set; }
  public string Reason { get; set; } = default!;
  public string Method { get; set; } = default!;
  public List<CreateRefundItemRequest> RefundItems { get; set; } = new();
}
