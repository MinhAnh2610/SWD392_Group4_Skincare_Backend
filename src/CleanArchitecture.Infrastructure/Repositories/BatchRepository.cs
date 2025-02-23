using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BatchRepository : GenericRepository<Batch>, IBatchRepository
{
  public BatchRepository(ApplicationDbContext context) : base(context)
  {
    
  }
  public async Task<List<Batch>> GetListByAnyId(
      Expression<Func<Batch, bool>> predicate,
      int level )
  {
    if (level < 1) level = 1;

    var entityType = _context.Model.FindEntityType(typeof(Batch));
    var query = _context.Set<Batch>().AsQueryable();
    var includePaths = new List<string>();

    void CollectPaths(IEntityType currentEntityType, int currentDepth, string currentPath)
    {
      foreach (var navigation in currentEntityType.GetNavigations())
      {
        var path = string.IsNullOrEmpty(currentPath)
            ? navigation.Name
            : $"{currentPath}.{navigation.Name}";

        includePaths.Add(path);

        if (currentDepth < level)
        {
          CollectPaths(navigation.TargetEntityType, currentDepth + 1, path);
        }
      }
    }

    CollectPaths(entityType, 1, "");

    foreach (var path in includePaths)
    {
      query = query.Include(path);
    }

    var entities = await query.Where(predicate).ToListAsync();

    foreach (var entity in entities)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entities;
  }
}
