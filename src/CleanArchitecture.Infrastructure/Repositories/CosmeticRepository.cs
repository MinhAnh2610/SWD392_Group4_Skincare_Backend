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
    var cosmetic = await _context.Cosmetics
        .FirstOrDefaultAsync(cosmetic => cosmetic.Id == id);

    return cosmetic;
  }

  public async Task<decimal> GetCosmeticPrice(Cosmetic cosmetic)
  {
    var price = await (
        from cosmeticPrice in _context.CosmeticPrices
        join events in _context.Events on cosmeticPrice.EventId equals events.Id into eventGroup
        from e in eventGroup.DefaultIfEmpty()
        where cosmeticPrice.CosmeticId == cosmetic.Id
        select cosmeticPrice.OriginalPrice * (e != null ? (100m - e.DiscountPercentage) / 100m : 1m)
    ).FirstOrDefaultAsync();

    return (decimal)(price != 0m ? price : 0m); // Return 0 if no price exists, adjust as per business logic
  }

  public async Task<decimal> GetCosmeticOriginalPrice(Cosmetic cosmetic)
  {
    var originalPrice = await (
      from cosmeticPrice in _context.CosmeticPrices
      where cosmeticPrice.CosmeticId == cosmetic.Id
      select cosmeticPrice.OriginalPrice
    ).FirstOrDefaultAsync();
    
    return (originalPrice != 0m ? originalPrice : 0m);
  }

  public async Task<decimal> GetCartItemPriceByCart(Cart cart)
  {
    var price = await (
      from carts in _context.Carts
      join cartItems in _context.CartItems on carts.Id equals cartItems.CartId
      join cosmeticPrices in _context.CosmeticPrices on cartItems.CosmeticId equals cosmeticPrices.CosmeticId
      join events in _context.Events on cosmeticPrices.EventId equals events.Id
      where carts.Id == cart.Id
      group new { carts, cartItems, cosmeticPrices, events } by new { cartItems.CosmeticId, events.Name, cosmeticPrices, events }
      into g
      select g.Sum(x => x.cosmeticPrices.OriginalPrice * (100 - x.events.DiscountPercentage) / 100)
      ).FirstOrDefaultAsync();
    return (decimal)price;
  }

  public async Task<List<Cosmetic>> GetCosmeticsByCart(Cart cart)
  {
    var cosmeticList = await (
      from carts in _context.Carts
      join cartItems in _context.CartItems on carts.Id equals cartItems.CartId
      join cosmetics in _context.Cosmetics on cartItems.CosmeticId equals cosmetics.Id
      where carts.Id == cart.Id
      select cosmetics
      ).ToListAsync();

    return cosmeticList;
  }

  public async Task<List<Cosmetic>> GetCosmeticsByOrder(Order order)
  {
    var cosmeticList = await(
      from orders in _context.Orders
      join orderItems in _context.OrderItems on orders.Id equals orderItems.OrderId
      join cosmetics in _context.Cosmetics on orderItems.CosmeticId equals cosmetics.Id
      where orders.Id == order.Id
      select cosmetics
      ).ToListAsync();

    return cosmeticList;
  }

  public async Task<List<Cosmetic>> GetCosmeticsByRoutine(Routine routine)
  {
    var cosmeticList = await (
      from routines in _context.Routines
      join routineSteps in _context.RoutineSteps on routines.Id equals routineSteps.RoutineId
      join cosmetics in _context.Cosmetics on routineSteps.CosmeticId equals cosmetics.Id
      where routines.Id == routine.Id
      select cosmetics
      ).ToListAsync();

    return cosmeticList;
  }
}
