using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
  public BrandRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<Brand>> GetAllBrandsAsync()
  {
    return await _context.Brands
      .Include(c => c.Cosmetics)
      .ToListAsync();
  }
}
