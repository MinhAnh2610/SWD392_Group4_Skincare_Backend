using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.DTOs.CosmeticSubcategory;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Mapster;
using System.Configuration;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticRepository : GenericRepository<Cosmetic>, ICosmeticRepository
{
  public CosmeticRepository(ApplicationDbContext context) : base(context)
  {

  }
  public override async Task<List<Cosmetic>> GetAllAsync()
  {
    return await _context.Cosmetics
      .Include(c => c.SkinType)
      .Include(c => c.CosmeticSubcategories)
        .ThenInclude(cs => cs.SubCategory)
      .ToListAsync();
  }

  public async Task<List<Cosmetic>> GetListByAnyId(Expression<Func<Cosmetic, bool>> predicate)
  {
    var entities = await _context.Set<Cosmetic>()
      .Where(predicate)
      .ProjectToType<CosmeticResponse>()
      .ToListAsync();
    foreach (var entity in entities)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    var cosmetics = entities.Adapt<List<Cosmetic>>();
    return cosmetics;
  }
  public override async Task<List<Cosmetic>> GetAllAsync()
  {
    var cosmeticsdtos = await _context.Cosmetics
        .ProjectToType<CosmeticResponse>()  
        .ToListAsync();
    var cosmetics = cosmeticsdtos.Adapt<List<Cosmetic>>();
    return cosmetics;

  }
  public override async Task<Cosmetic?> GetByIdAsync(Guid id)
  {
    var cosmeticsdtos = await _context.Cosmetics
        .Where(c => c.Id == id)
        .ProjectToType<CosmeticResponse>()
        .FirstOrDefaultAsync();
    var cosmetics = cosmeticsdtos.Adapt<Cosmetic>();
    return cosmetics;

  }


}
