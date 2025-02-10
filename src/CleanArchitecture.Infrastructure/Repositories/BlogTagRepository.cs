using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BlogTagRepository : GenericRepository<BlogTag>, IBlogTagRepository
{
  public BlogTagRepository(ApplicationDbContext context) : base(context)
  {
  }
}
