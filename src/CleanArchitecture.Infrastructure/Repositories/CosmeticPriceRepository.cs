using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class CosmeticPriceRepository : GenericRepository<CosmeticPrice>, ICosmeticPriceRepository
  {
    public CosmeticPriceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<CosmeticPrice> GetQueryable()
    {
      return base.GetQueryable();
    }

    public async Task<CosmeticPrice?> GetByCosmeticIdAsync(Guid cosmeticId)
    {
      return await _context.CosmeticPrices.Where(c => c.CosmeticId == cosmeticId).FirstOrDefaultAsync(); 
    }
    //TODO: HERE
    // public Task<CosmeticPrice?> GetByCosmeticIdAsync(Guid cosmeticId)
    // {
    // }
  }
}