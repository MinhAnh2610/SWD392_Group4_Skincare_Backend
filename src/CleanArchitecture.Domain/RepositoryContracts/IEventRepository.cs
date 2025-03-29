namespace CleanArchitecture.Domain.RepositoryContracts
{
  public interface IEventRepository : IGenericRepository<Event>
  {
    Task<int> ApplyEventAsync(Event eventItem);
    Task<Event?> GetEventByNameAsync(string eventName);
  }
}