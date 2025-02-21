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

  public override async Task<List<Cosmetic>> GetAllAsync()
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
