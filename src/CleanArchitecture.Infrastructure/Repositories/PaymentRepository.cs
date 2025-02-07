using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(string status)
        {
            return await _context.Set<Payment>()
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Payment>()
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAmountByStatusAsync(string status)
        {
            return await _context.Set<Payment>()
                .Where(p => p.Status == status)
                .SumAsync(p => p.TotalAmount);
        }
    }
}