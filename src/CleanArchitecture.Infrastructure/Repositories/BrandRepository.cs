
namespace CleanArchitecture.Infrastructure.Repositories;

public class BrandRepository : GenericRepository<Brand>
{
  public BrandRepository(ApplicationDbContext context) : base(context)
  {
  }
}
