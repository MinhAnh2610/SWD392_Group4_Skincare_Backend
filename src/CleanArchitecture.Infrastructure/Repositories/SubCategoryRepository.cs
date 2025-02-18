using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
  public SubCategoryRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
  {
    return await _context.SubCategories
      .Include(sc => sc.CosmeticSubcategories)
      .ToListAsync();
  }
}
