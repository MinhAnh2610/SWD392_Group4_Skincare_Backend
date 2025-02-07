using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RefundItemRepository :  GenericRepository<RefundItem>, IRefundItemRepository
    {
        public RefundItemRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<RefundItem>> GetRefundItemsByReason(string reason)
        {
            return await _context.Set<RefundItem>()
                .Where(ri => ri.Reason == reason)
                .ToListAsync();
        }

        public async Task<int> GetTotalQuantityByReason(string reason)
        {
            return await _context.Set<RefundItem>()
                .Where(ri => ri.Reason == reason)
                .SumAsync(ri => ri.Quantity);
        }
    }
}
