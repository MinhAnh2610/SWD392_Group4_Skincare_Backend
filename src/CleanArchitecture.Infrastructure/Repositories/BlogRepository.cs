
using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
  public BlogRepository(ApplicationDbContext context) : base(context)
  {
  }
}
