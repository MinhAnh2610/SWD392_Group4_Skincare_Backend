using CleanArchitecture.Application.DTOs.SkinTypeDto;

namespace CleanArchitecture.Presentation.Endpoints;

public class SkinTypeController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/skin-type").WithTags("SkinType Management");

    #region Get SkinTypes API
    group.MapGet("/", async (ISkinTypeService service) =>
    {
      var result = await service.GetSkinTypesAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<SkinTypeResponse>>.SuccessResponse(result.Data!, "Retrieved Skikn Types Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetSkinTypes")
    .Produces<ApiResponse<List<SkinTypeResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetSkinTypes")
    .WithDescription("Get Skin Types");
    #endregion
  }
}
