using System.Linq.Expressions;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICosmeticRepository : IGenericRepository<Cosmetic>
{
  Task<List<Cosmetic>> GetListByAnyId(Expression<Func<Cosmetic, bool>> predicate);
  Task<decimal> GetCosmeticPrice(Cosmetic cosmetic);
  Task<decimal> GetCartItemPriceByCart(Cart cart);
  Task<List<Cosmetic>> GetCosmeticsByCart(Cart cart);
  Task<List<Cosmetic>> GetCosmeticsByOrder(Order order);
  Task<List<Cosmetic>> GetCosmeticsByRoutine(Routine routine);
}
