namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticTypeRepository : GenericRepository<CosmeticType>
{
  public CosmeticTypeRepository(ApplicationDbContext context) : base(context)
  {
  }
}
