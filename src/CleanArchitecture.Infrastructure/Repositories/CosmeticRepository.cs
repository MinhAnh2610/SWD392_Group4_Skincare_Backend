using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticRepository : GenericRepository<Cosmetic>, ICosmeticRepository
{
  public CosmeticRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<Cosmetic>> GetCosmeticsAsync()
  {
    return await _context.Cosmetics
      .Include(c => c.Brand)
      .Include(c => c.SkinType)
      .Include(c => c.CosmeticType)
      .Include(c => c.CosmeticSubcategories)
        .ThenInclude(cs => cs.SubCategory)
      .Include(c => c.CosmeticImages)
      .Include(c => c.Feedbacks)
        .ThenInclude(f => f.Customer)
      .ToListAsync();
  }
}
