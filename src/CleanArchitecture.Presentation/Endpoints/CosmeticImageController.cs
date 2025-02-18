using CleanArchitecture.Application.DTOs.CosmeticImageDto;

namespace CleanArchitecture.Presentation.Endpoints;

public class CosmeticImageController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/cosmetic-image").WithTags("Cosmetic Images Management");

    #region Get Cosmetic Images API
    group.MapGet("/", async (ICosmeticImageService service) =>
    {
      var result = await service.GetCosmeticImagesAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<CosmeticImageResponse>>.SuccessResponse(result.Data!, "Retrieved Cosmetic Images Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetCosmeticImages")
    .Produces<ApiResponse<List<CosmeticImageResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetCosmeticImages")
    .WithDescription("Get Cosmetic Images");
    #endregion
  }
}
