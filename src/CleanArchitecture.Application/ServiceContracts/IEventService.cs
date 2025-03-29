using CleanArchitecture.Application.DTOs.Events;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IEventService
  {
    Task<Result<List<EventResponse>>> GetAllEventsAsync();
    Task<Result<EventResponse>> ApplyEventAsync(string eventName);
    Task<Result<EventResponse>> CreateEventAsync(CreateEventRequest request);
  }
}