using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IRefundItemRepository : IGenericRepository<RefundItem>
  {
      Task<IEnumerable<RefundItem>> GetRefundItemsByReason(string reason);
      Task<int> GetTotalQuantityByReason(string reason);
  }
