using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.RepositoryContracts
{
    public interface IRefundItemRepository : IGenericRepository<RefundItem>
    {
        Task<IEnumerable<RefundItem>> GetRefundItemsByReason(string reason);
        Task<int> GetTotalQuantityByReason(string reason);
    }
}
