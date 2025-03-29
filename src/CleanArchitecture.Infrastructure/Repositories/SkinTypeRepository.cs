using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SkinTypeRepository : GenericRepository<SkinType>, ISkinTypeRepository
{
  public SkinTypeRepository(ApplicationDbContext context) : base(context)
  {
    
  }

  public async Task<SkinType?> FindSkinTypeBasedOnBaumannAsync(string name)
  {
    var skinType = await _context.SkinTypes.Where(st => st.Name == name).FirstOrDefaultAsync();
    return (skinType != null) ? skinType : default;
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
