namespace CleanArchitecture.Application.DTOs.Refund;

public class ReviewRefundRequest
{
  public bool Approved { get; set; }
  public Guid StaffId { get; set; }
}
