using CleanArchitecture.Application.DTOs.BrandDto;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Presentation.Endpoints;

public class BrandController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/brand").WithTags("Brands Management");

    #region Get Brands API
    group.MapGet("/", async (IBrandService service) =>
    {
      var result = await service.GetBrandsAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<BrandResponse>>.SuccessResponse(result.Data!, "Retrieved Brands Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetBrands")
    .Produces<ApiResponse<List<BrandResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetBrands")
    .WithDescription("Get Brands");
    #endregion
  }
}
