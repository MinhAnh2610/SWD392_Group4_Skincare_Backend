using CleanArchitecture.Application.DTOs.RoutineDTO;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class RoutineController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("/api/routine").WithTags("Routines Management");

      // GET /routines → Retrieve all routines
      group.MapGet("/", async (IRoutineService routineService) =>
      {
        var result = await routineService.GetAllRoutinesAsync();
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<RoutineResponse>>.SuccessResponse(
              result.Data!,
              "Retrieved all routines successfully."
          ));
        }
        return Results.StatusCode(result.Status);
      })
      .WithName("GetAllRoutines")
      .Produces<ApiResponse<List<RoutineResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("GetAllRoutines")
      .WithDescription("Get All Routines");

      group.MapGet("/skin-type/{id}", async (IRoutineService service, [FromRoute] Guid id) =>
      {
        var result = await service.GetRoutinesBasedOnSkinType(id);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<RoutineResponse>>.SuccessResponse(
              result.Data!,
              "Retrieved all routines successfully."
          ));
        }
        return Results.StatusCode(result.Status);
      })
      .WithName("GetRoutinesBasedOnSkinType")
      .Produces<ApiResponse<List<RoutineResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("GetRoutinesBasedOnSkinType")
      .WithDescription("Get Routines Based On Skin Type");
    }
  }
}
