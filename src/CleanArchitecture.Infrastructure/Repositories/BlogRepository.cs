using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
  public BlogRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override IQueryable<Blog> GetQueryable()
  {
    return base.GetQueryable().Include(b => b.Staff);
  }
}