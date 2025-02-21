using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticImageRepository : GenericRepository<CosmeticImage>, ICosmeticImageRepository
{
  public CosmeticImageRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<List<CosmeticImage>> GetAllAsync()
  {
    return await _context.CosmeticsImages
      .Include(ci => ci.Cosmetic)
      .ToListAsync();
  }
}
