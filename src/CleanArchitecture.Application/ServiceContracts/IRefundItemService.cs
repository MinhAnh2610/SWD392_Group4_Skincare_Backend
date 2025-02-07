using CleanArchitecture.Application.DTOs.RefundItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
    public interface IRefundItemService
    {
        Task<Result<List<RefundItemResponse>>> GetAllRefundItemsAsync();
        Task<Result<RefundItemResponse>> GetRefundItemByIdAsync(Guid id);
        Task<Result<RefundItemResponse>> CreateRefundItemAsync(CreateRefundItemRequest request);
        Task<Result<RefundItemResponse>> UpdateRefundItemAsync(UpdateRefundItemRequest request);
        Task<Result<string>> DeleteRefundItemAsync(Guid id);
    }
}
