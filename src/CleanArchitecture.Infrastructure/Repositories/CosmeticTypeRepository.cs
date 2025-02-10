using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticTypeRepository : GenericRepository<CosmeticType>, ICosmeticTypeRepository
{
  public CosmeticTypeRepository(ApplicationDbContext context) : base(context)
  {
  }
}
