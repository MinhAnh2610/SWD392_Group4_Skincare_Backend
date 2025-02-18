using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticTypeRepository : GenericRepository<CosmeticType>, ICosmeticTypeRepository
{
  public CosmeticTypeRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<CosmeticType>> GetAllCosmeticTypesAsync()
  {
    return await _context.CosmeticTypes
      .Include(ct => ct.Cosmetics)
      .ToListAsync();
  }
}
