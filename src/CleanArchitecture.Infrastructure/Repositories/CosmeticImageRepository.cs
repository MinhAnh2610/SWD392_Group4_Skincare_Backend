using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticImageRepository : GenericRepository<CosmeticImage>, ICosmeticImageRepository
{
  public CosmeticImageRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<List<CosmeticImage>> GetAllAsync()
  {
    return await _context.CosmeticsImages
      .Include(ci => ci.Cosmetic)
      .ToListAsync();
  }

  public async Task<List<CosmeticImage>> GetCosmeticImagesByCosmeticId(Guid cosmeticId)
  {
    var images = await (
        from cosmeticImages in _context.CosmeticsImages
        where cosmeticImages.CosmeticId == cosmeticId
          select cosmeticImages
      ).ToListAsync();

    return images;
  }
}
