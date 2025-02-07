using CleanArchitecture.Application.DTOs.Refund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
    public interface IRefundService
    {
        Task<Result<List<RefundResponse>>> GetAllRefundsAsync();
        Task<Result<RefundResponse>> GetRefundByIdAsync(Guid id);
        Task<Result<RefundResponse>> CreateRefundAsync(CreateRefundRequest request);
        Task<Result<RefundResponse>> UpdateRefundAsync(UpdateRefundRequest request);
        Task<Result<string>> DeleteRefundAsync(Guid id);
    }
}
