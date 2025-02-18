using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
  public CategoryRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<Category>> GetCategoriesAsync()
  {
    return await _context.Categories
      .Include(c => c.SubCategories)
      .ToListAsync();
  }
}
