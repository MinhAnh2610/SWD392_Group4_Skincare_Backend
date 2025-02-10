using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
  public SubCategoryRepository(ApplicationDbContext context) : base(context)
  {
  }
}
