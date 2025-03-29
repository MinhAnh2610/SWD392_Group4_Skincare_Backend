using CleanArchitecture.Application.DTOs.Refund;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IRefundService
{
  Task<Result<RefundResponse>> CreateRefundAsync(CreateRefundRequest request);
  Task<Result<RefundResponse>> ReviewRefundAsync(ReviewRefundRequest request);
  Task<Result<RefundResponse>> ProcessRefundAsync(ProcessRefundRequest request);
}
