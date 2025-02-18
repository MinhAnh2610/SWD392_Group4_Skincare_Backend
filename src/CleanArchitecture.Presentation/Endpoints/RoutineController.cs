// File: Presentation/Endpoints/RoutineController.cs
using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.ServiceContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class RoutineController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("/routines").WithTags("Routines");

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
      .RequireAuthorization();
    }
  }
}
