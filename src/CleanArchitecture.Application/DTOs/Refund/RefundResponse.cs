using CleanArchitecture.Application.DTOs.RefundItem;

namespace CleanArchitecture.Application.DTOs.Refund;

public class RefundResponse
{
  public Guid Id { get; set; }
  public string? Reason { get; set; }
  public decimal TotalAmount { get; set; }
  public string? Status { get; set; }
  public string? Method { get; set; }
  public Guid? StaffId { get; set; }
  public string? StaffName { get; set; }
  public List<RefundItemResponse> RefundItems { get; set; } = new();
}
