using CleanArchitecture.Application.DTOs.Events;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class EventController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/events").WithTags("Events Management");

      #region Get Events API

      group.MapGet("/",
          async (IEventService service) =>
          {
            var result = await service.GetAllEventsAsync();
            return result.Match(Message.SUCCESSFUL_RETRIEVED("Events"));
          })
        .WithName("GetEvents")
        .Produces<ApiResponse<List<EventResponse>>>()
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("GetEvents")
        .WithDescription("Get Events");

      #endregion

      #region Apply Events API
      group.MapPatch(
          "/apply/{eventName}",
          async (IEventService service, [FromRoute] string eventName = "none") =>
          {
            var result = await service.ApplyEventAsync(eventName);
            return result.Match(Message.SUCCESSFUL_UPDATED("Events"));
          })
        .WithName("ApplyEvent")
        .Produces<ApiResponse<EventResponse>>()
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("ApplyEvent")
        .WithDescription("Apply Event");
      #endregion
      
      #region Create Events API
      group.MapPost(
          "/",
          async (IEventService service, [FromBody] CreateEventRequest request) =>
          {
            var result = await service.CreateEventAsync(request);
            return result.Match(Message.SUCCESSFUL_CREATED("Event"));
          })
        .WithName("CreateEvent")
        .Produces<ApiResponse<EventResponse>>()
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("CreateEvent")
        .WithDescription("Create Event");
      #endregion
    }
  }
}