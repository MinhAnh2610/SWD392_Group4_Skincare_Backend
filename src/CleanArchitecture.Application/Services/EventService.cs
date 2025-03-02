using CleanArchitecture.Application.DTOs.Events;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.RepositoryContracts;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class EventService : IEventService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IErrorFactory _errorFactory;
    private readonly ITimeZoneService _timeZoneService;
    private readonly IValidator<CreateEventRequest> _createEventRequestValidator;

    public EventService(ITimeZoneService timeZoneService, IErrorFactory errorFactory, IUnitOfWork unitOfWork, IValidator<CreateEventRequest> createEventRequestValidator)
    {
      _timeZoneService = timeZoneService;
      _errorFactory = errorFactory;
      _unitOfWork = unitOfWork;
      _createEventRequestValidator = createEventRequestValidator;
    }

    public async Task<Result<List<EventResponse>>> GetAllEventsAsync()
    {
      var events = await _unitOfWork.Events.GetAllAsync();
      
      return Result<List<EventResponse>>.Success(events.Adapt<List<EventResponse>>(), StatusCodes.Status200OK);
    }

    public async Task<Result<EventResponse>> ApplyEventAsync(string eventName)
    {
      var existingEvent = await _unitOfWork.Events.GetEventByNameAsync(eventName);

      if (existingEvent is null)
      {
        var error = _errorFactory.CreateNotFoundError("Event");
        return Result<EventResponse>.Failure([error.err], error.statusCode);
      }

      await _unitOfWork.Events.ApplyEventAsync(existingEvent);

      var isSaved = await _unitOfWork.CompleteAsync();

      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Event");
        return Result<EventResponse>.Failure([error.err], error.statusCode);
      }
      
      return Result<EventResponse>.Success(existingEvent.Adapt<EventResponse>(), StatusCodes.Status200OK);
    }

    public async Task<Result<EventResponse>> CreateEventAsync(CreateEventRequest request)
    {
      var validationResult = await _createEventRequestValidator.ValidateAsync(request);
      if (!validationResult.IsValid)
      {
        var error = _errorFactory.CreateValidationError("Event", validationResult);
        return Result<EventResponse>.Failure(error.errs, error.statusCode);
      }
      var existingEvent = await _unitOfWork.Events.GetEventByNameAsync(request.Name);

      if (existingEvent is not null)
      {
        var error = _errorFactory.CreateAlreadyExistsError("Event");
        return Result<EventResponse>.Failure([error.err], error.statusCode);
      }

      _unitOfWork.Events.Create(request.Adapt<Event>());
      
      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Event");
        return Result<EventResponse>.Failure([error.err], error.statusCode);
      }
      
      return Result<EventResponse>.Success(request.Adapt<EventResponse>(), StatusCodes.Status201Created);
    }
  }
}