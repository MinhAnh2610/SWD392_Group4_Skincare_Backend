using System.Linq.Expressions;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICosmeticRepository : IGenericRepository<Cosmetic>
{
  Task<List<Cosmetic>> GetListByAnyId(Expression<Func<Cosmetic, bool>> predicate);
  public Task<List<Cosmetic>> GetAllAsync();
}
