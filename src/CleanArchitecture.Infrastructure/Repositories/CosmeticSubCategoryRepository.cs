using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticSubCategoryRepository : GenericRepository<CosmeticSubCategory>, ICosmeticSubCategoryRepository
{
  public CosmeticSubCategoryRepository(ApplicationDbContext context) : base(context)
  {
  }
}
