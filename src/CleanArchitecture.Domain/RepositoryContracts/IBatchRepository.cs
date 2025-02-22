using System.Linq.Expressions;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IBatchRepository : IGenericRepository<Batch>
{
  Task<List<Batch>> GetListByAnyId(Expression<Func<Batch, bool>> predicate);
}
