
namespace CleanArchitecture.Infrastructure.Repositories;

public class BlogRepository : GenericRepository<Blog>
{
  public BlogRepository(ApplicationDbContext context) : base(context)
  {
  }
}
