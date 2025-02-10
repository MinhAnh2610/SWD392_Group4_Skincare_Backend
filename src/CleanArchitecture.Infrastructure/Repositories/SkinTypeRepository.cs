using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SkinTypeRepository : GenericRepository<SkinType>, ISkinTypeRepository
{
  public SkinTypeRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
