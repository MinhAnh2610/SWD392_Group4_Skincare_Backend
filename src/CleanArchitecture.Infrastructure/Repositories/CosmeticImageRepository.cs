using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticImageRepository : GenericRepository<CosmeticImage>, ICosmeticImageRepository
{
  public CosmeticImageRepository(ApplicationDbContext context) : base(context)
  {
  }
}
