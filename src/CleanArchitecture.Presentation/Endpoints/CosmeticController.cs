using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints;

public class CosmeticController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/cosmetic").WithTags("Cosmetics Management");

    #region Get Cosmetics API
    group.MapGet("/get-all", async (ICosmeticService service) =>
    {
      var result = await service.GetAllCosmetics();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<CosmeticResponse>>.SuccessResponse(result.Data!, "Retrieved Cosmetics Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetCosmetics")
    .Produces<ApiResponse<List<CosmeticResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetCosmetics")
    .WithDescription("Get Cosmetics");
    #endregion

    #region Get Cosmetic By Id API
    group.MapGet("/get-by-Id", async (ICosmeticService service, [FromQuery]Guid id) =>
    {
      var result = await service.GetCosmeticById(id);
      if (result != null)
      {
        return Results.Ok(ApiResponse<CosmeticResponse>.SuccessResponse(result.Data!, "Retrieved Cosmetic Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetCosmeticsById")
    .Produces<ApiResponse<CosmeticResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetCosmeticsById")
    .WithDescription("Get Cosmetics By Id");
    #endregion

    #region Update Cosmetic  API
    group.MapPut("/{id}/update", async (ICosmeticService service, UpdateCosmetic updateRequest) =>
    {
      var result = await service.UpdateCosmetic(updateRequest);
      if (result != null)
      {
        return Results.Ok(ApiResponse<CosmeticResponse>.SuccessResponse(result.Data!, "Update Cosmetic Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("UpdateCosmetic")
    .Produces<ApiResponse<CosmeticResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("UpdateCosmeticById")
    .WithDescription("Update Cosmetic By Id");
    #endregion

    #region Delete Cosmetic By Id API
    group.MapPut("/{id}/delete", async (ICosmeticService service, Guid id) =>
    {
      var result = await service.DeleteCosmetic(id);
      if (result != null)
      {
        return Results.Ok(ApiResponse<CosmeticResponse>.SuccessResponse(result.Data!, "Delete Cosmetic Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("DeleteCosmeticById")
    .Produces<ApiResponse<CosmeticResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("DeleteCosmeticById")
    .WithDescription("Delete Cosmetic By Id");
    #endregion

    #region Create Cosmetic By Id API
    group.MapPost("/create", async (ICosmeticService service, CreateCosmetic cosmetic) =>
    {
      var result = await service.CreateCosmetic(cosmetic);
      if (result != null)
      {
        return Results.Ok(ApiResponse<CosmeticResponse>.SuccessResponse(result.Data!, "Create Cosmetic Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("CreateCosmeticById")
    .Produces<ApiResponse<CosmeticResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CreateCosmeticById")
    .WithDescription("CreateCosmetic By Id");
    #endregion

    #region Filter Cosmetic  API
    group.MapGet("/filter", async (ICosmeticService service, [AsParameters] FilterCosmeticRequest request) =>
    {
      var result = await service.SearchCosmetics(request);
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<CosmeticResponse>>.SuccessResponse(result.Data!, "Search Cosmetic Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("FilterCosmetic")
    .Produces<ApiResponse<List<CosmeticResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("FilterCosmetic")
    .WithDescription("FilterCosmetic");
    #endregion
  }
}

