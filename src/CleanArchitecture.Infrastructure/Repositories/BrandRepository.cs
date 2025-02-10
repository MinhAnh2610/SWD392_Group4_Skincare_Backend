using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
  public BrandRepository(ApplicationDbContext context) : base(context)
  {
  }
}
