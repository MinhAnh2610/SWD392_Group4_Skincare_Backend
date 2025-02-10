
namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticImageRepository : GenericRepository<CosmeticImage>
{
  public CosmeticImageRepository(ApplicationDbContext context) : base(context)
  {
  }
}
