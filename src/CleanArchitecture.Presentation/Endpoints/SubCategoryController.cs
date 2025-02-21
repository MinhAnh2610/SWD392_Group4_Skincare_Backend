
using CleanArchitecture.Application.DTOs.BlogDto;
using CleanArchitecture.Application.DTOs.SubCategoryDto;

namespace CleanArchitecture.Presentation.Endpoints;

public class SubCategoryController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/sub-category").WithTags("SubCategories Management");

    #region Get SubCategories API
    group.MapGet("/", async (ISubCategoryService service) =>
    {
      var result = await service.GetAllSubCategoriesAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<SubCategoryResponse>>.SuccessResponse(result.Data!, "Retrieved SubCategories Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetSubCategories")
    .Produces<ApiResponse<List<SubCategoryResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetSubCategories")
    .WithDescription("Get SubCategories");
    #endregion
  }
}
