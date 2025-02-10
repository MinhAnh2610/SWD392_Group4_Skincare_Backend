namespace CleanArchitecture.Infrastructure.Repositories;

public class BlogTagRepository : GenericRepository<BlogTag>
{
  public BlogTagRepository(ApplicationDbContext context) : base(context)
  {
  }
}
