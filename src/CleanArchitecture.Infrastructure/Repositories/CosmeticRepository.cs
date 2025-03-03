using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Domain.RepositoryContracts;
using Mapster;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticRepository : GenericRepository<Cosmetic>, ICosmeticRepository
{
  public CosmeticRepository(ApplicationDbContext context) : base(context)
  {

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

  public async Task<decimal> GetCosmeticPrice(Cosmetic cosmetic)
  {
    var price = await(
      from cosmeticPrice in _context.CosmeticPrices
      join events in _context.Events on cosmeticPrice.EventId equals events.Id
      where cosmeticPrice.CosmeticId == cosmetic.Id
      select (cosmeticPrice.OriginalPrice * (100 - events.DiscountPercentage)) / 100
      ).FirstOrDefaultAsync();

    return (decimal)price;
  }

}
