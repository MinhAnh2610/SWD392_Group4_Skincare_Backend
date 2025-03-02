using CleanArchitecture.Application.DTOs.Events;
using Mapster;

namespace CleanArchitecture.Application.Mapper
{
  public class EventMappingConfig : IRegister 
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<Event, EventResponse>();
      config.NewConfig<Event, CreateEventRequest>();
    }
  }
}