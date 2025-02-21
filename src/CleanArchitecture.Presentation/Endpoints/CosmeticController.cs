using CleanArchitecture.Application.DTOs.CosmeticDto;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Presentation.Endpoints;

public class CosmeticController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/cosmetic").WithTags("Cosmetics Management");

    #region Get Cosmetics API
    group.MapGet("/", async (ICosmeticService service) =>
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
  }
}
