
namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticRepository : GenericRepository<Cosmetic>
{
  public CosmeticRepository(ApplicationDbContext context) : base(context)
  {
  }
}
