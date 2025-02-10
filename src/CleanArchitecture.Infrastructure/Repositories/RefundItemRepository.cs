using CleanArchitecture.Domain.RepositoryContracts;

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
