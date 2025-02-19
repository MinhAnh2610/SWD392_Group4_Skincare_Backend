using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SkinTypeRepository : GenericRepository<SkinType>, ISkinTypeRepository
{
  public SkinTypeRepository(ApplicationDbContext context) : base(context)
  {
    
  }

  public override async Task<List<SkinType>> GetAllAsync()
  {
    return await _context.SkinTypes
      .Include(st => st.Customers)
      .Include(st => st.Cosmetics)
      .Include(st => st.Routines)
      .ToListAsync();
  }
}
