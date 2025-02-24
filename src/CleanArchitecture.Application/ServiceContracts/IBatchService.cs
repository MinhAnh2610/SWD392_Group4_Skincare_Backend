
using CleanArchitecture.Application.DTOs.BatchDto;
using CleanArchitecture.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IBatchService
  {
    Task<Result<List<BatchResponse>>> GetAllBatches();
    Task<Result<BatchResponse>> GetBatchById(Guid id);
    Task<Result<List<BatchResponse>>> GetBatchesByCosmeticId(Guid cosId);
    Task<Result<BatchResponse>> CreateBatch(BatchCreateRequest batch);
    Task<Result<BatchResponse>> UpdateBatch(BatchUpdateRequest cosmetic, Guid id);
    Task<Result<BatchResponse>> DeleteBatch(Guid id);
    Task<Result<List<BatchResponse>>> GetBatchesByDateRangeAsync(DateOnly startDate, DateOnly endDate,
    DateSearchType searchTypes);

  }
  [Flags]
  public enum DateSearchType
  {
    None = 0,
    Exported = 1,
    Manufacture = 2,
    Expiration = 4
  }
}
