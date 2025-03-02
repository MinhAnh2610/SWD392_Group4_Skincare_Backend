using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class EventRepository : GenericRepository<Event>, IEventRepository
  {
    public EventRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<int> ApplyEventAsync(Event eventItem)
    {
      return await _context
        .CosmeticPrices
        .ExecuteUpdateAsync(s =>
          s.SetProperty(
            price => price.EventId,
            price => eventItem.Id));
    }

    public async Task<Event?> GetEventByNameAsync(string eventName)
    {
      return await _context.Events.FirstOrDefaultAsync(eve => eve.Name.ToLower() == eventName.ToLower());
    }
  }
}