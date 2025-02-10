
namespace CleanArchitecture.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>
{
  public CategoryRepository(ApplicationDbContext context) : base(context)
  {
  }
}
