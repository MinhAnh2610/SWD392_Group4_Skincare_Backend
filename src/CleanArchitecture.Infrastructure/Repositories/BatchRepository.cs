using CleanArchitecture.Domain.RepositoryContracts;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BatchRepository : GenericRepository<Batch>, IBatchRepository
{
  public BatchRepository(ApplicationDbContext context) : base(context)
  {
    
  }
  public async Task<List<Batch>> GetListByAnyId(Expression<Func<Batch, bool>> predicate)
  {
    var entities = await _context.Set<Batch>().Where(predicate).ToListAsync();
    foreach (var entity in entities)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entities;
  }
}
