using CleanArchitecture.Domain.RepositoryContracts;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticRepository : GenericRepository<Cosmetic>, ICosmeticRepository
{
  public CosmeticRepository(ApplicationDbContext context) : base(context) 
  {

  }
  public async Task<List<Cosmetic>> GetListByAnyId(Expression<Func<Cosmetic, bool>> predicate)
  {
    var entities = await _context.Set<Cosmetic>().Where(predicate).ToListAsync();
    foreach (var entity in entities)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entities;
  }

  public async Task<List<Cosmetic>> GetCosmeticsAsync()
  {
    return await _context.Cosmetics
      .Include(c => c.Brand)
      .Include(c => c.SkinType)
      .Include(c => c.CosmeticType)
      .ToListAsync();
  }
}
