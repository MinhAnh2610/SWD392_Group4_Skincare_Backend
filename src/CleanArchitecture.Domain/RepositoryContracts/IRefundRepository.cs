using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IRefundRepository : IGenericRepository<Refund>
  {
      Task<IEnumerable<Refund>> GetRefundsByStatus(string status);
      Task<IEnumerable<Refund>> GetRefundsByDateRange(DateTime startDate, DateTime endDate);
      Task<decimal> GetTotalRefundAmount(string status);
  }
