namespace CleanArchitecture.Infrastructure.Repositories;

public class SkinTypeRepository : GenericRepository<SkinType>
{
  public SkinTypeRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
