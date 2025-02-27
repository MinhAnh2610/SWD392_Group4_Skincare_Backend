using CleanArchitecture.Application.DTOs.BatchDto;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Presentation.Endpoints;

public class BatchController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/batch").WithTags("Batch Management");

    #region Create Batch
    group.MapPost("/create", async (IBatchService service, BatchCreateRequest request) =>
    {
      var result = await service.CreateBatch(request);
      return result.IsSuccess
          ? Results.Ok(ApiResponse<BatchResponse>.SuccessResponse(result.Data!, "Batch created successfully"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("CreateBatch")
    .Produces<ApiResponse<BatchResponse>>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Create new batch")
    .WithDescription("Creates a new cosmetic batch entry");
    #endregion

    #region Get All Batches
    group.MapGet("/get-all", async (IBatchService service) =>
    {
      var result = await service.GetAllBatches();
      return result.IsSuccess
          ? Results.Ok(ApiResponse<List<BatchResponse>>.SuccessResponse(result.Data!, "Batches retrieved"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetAllBatches")
    .Produces<ApiResponse<List<BatchResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Get all batches")
    .WithDescription("Retrieves all available batches");
    #endregion

    #region Get Batch by ID
    group.MapGet("/{id}", async (IBatchService service, Guid id) =>
    {
      var result = await service.GetBatchById(id);
      return result.IsSuccess
          ? Results.Ok(ApiResponse<BatchResponse>.SuccessResponse(result.Data!, "Batch found"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetBatchById")
    .Produces<ApiResponse<BatchResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Get batch by ID")
    .WithDescription("Retrieves a single batch by its GUID");
    #endregion

    #region Get Batches by Cosmetic ID
    group.MapGet("/by-cosmetic/{cosmeticId}", async (IBatchService service, Guid cosmeticId) =>
    {
      var result = await service.GetBatchesByCosmeticId(cosmeticId);
      return result.IsSuccess
          ? Results.Ok(ApiResponse<List<BatchResponse>>.SuccessResponse(result.Data!, "Batches found"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetBatchesByCosmeticId")
    .Produces<ApiResponse<List<BatchResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Get batches by cosmetic")
    .WithDescription("Retrieves all batches associated with a cosmetic");
    #endregion

    #region Update Batch
    group.MapPut("/{id}", async (IBatchService service, Guid id, BatchUpdateRequest request) =>
    {
      var result = await service.UpdateBatch(request, id);
      return result.IsSuccess
          ? Results.Ok(ApiResponse<BatchResponse>.SuccessResponse(result.Data!, "Batch updated"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("UpdateBatch")
    .Produces<ApiResponse<BatchResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Update batch")
    .WithDescription("Updates an existing batch entry");
    #endregion

    #region Delete Batch
    group.MapDelete("/{id}", async (IBatchService service, Guid id) =>
    {
      var result = await service.DeleteBatch(id);
      return result.IsSuccess
          ? Results.Ok(ApiResponse<BatchResponse>.SuccessResponse(result.Data!, "Batch deleted"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("DeleteBatch")
    .Produces<ApiResponse<BatchResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Delete batch")
    .WithDescription("Permanently deletes a batch entry");
    #endregion

    #region Batch Date Range Search
    group.MapGet("/search", async (IBatchService service,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        [FromQuery] string searchTypes = "Exported,Manufacture,Expiration") =>
    {
          if (startDate > endDate)
            {
                return Results.BadRequest(ApiResponse<List<BatchResponse>>.FailureResponse([BatchErrors.WrongDateRange],
        "Start date cannot be after end date"));
            }
      
          if (!Enum.TryParse<DateSearchType>(searchTypes, out var parsedTypes))
            {
              return Results.BadRequest(ApiResponse<List<BatchResponse>>.FailureResponse([BatchErrors.WrongSearchType],
        "Invalid search types provided"));
           }
      var result = await service.GetBatchesByDateRangeAsync(startDate, endDate, parsedTypes);
      return result.IsSuccess
          ? Results.Ok(ApiResponse<List<BatchResponse>>.SuccessResponse(result.Data!, "Batches found"))
          : Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("SearchBatchesByDate")
    .Produces<ApiResponse<List<BatchResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Search batches by date range")
    .WithDescription("Searches batches within date ranges across multiple date fields");
    #endregion
  }
}