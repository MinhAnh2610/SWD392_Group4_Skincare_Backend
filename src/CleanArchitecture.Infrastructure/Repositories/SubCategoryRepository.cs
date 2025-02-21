using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
  public SubCategoryRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<List<SubCategory>> GetAllAsync()
  {
    return await _context.SubCategories
      .Include(sc => sc.CosmeticSubcategories)
      .ToListAsync();
  }
}
