using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RefundRepository : GenericRepository<Refund>, IRefundRepository
    {
        public RefundRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Refund>> GetRefundsByStatus(string status)
        {
            return await _context.Set<Refund>()
                .Where(r => r.Status == status)
            .ToListAsync();
        }

        public async Task<IEnumerable<Refund>> GetRefundsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Refund>()
                .Where(r => r.RequestedDate >= startDate && r.RequestedDate <= endDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRefundAmount(string status)
        {
            return await _context.Set<Refund>()
                .Where(r => r.Status == status)
                .SumAsync(r => r.TotalAmount);
        }
    }
}
