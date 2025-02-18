
using CleanArchitecture.Application.DTOs.CosmeticTypeDto;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Presentation.Endpoints;

public class CosmeticTypeController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/cosmetic-type").WithTags("Cosmetic Type Management");

    #region Get Cosmetic Types API
    group.MapGet("/", async (ICosmeticTypeService service) =>
    {
      var result = await service.GetCosmeticTypesAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<CosmeticTypeResponse>>.SuccessResponse(result.Data!, "Retrieved Cosmetic Types Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetCosmeticTypes")
    .Produces<ApiResponse<List<CosmeticTypeResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetCosmeticTypes")
    .WithDescription("Get Cosmetic Types");
    #endregion
  }
}
