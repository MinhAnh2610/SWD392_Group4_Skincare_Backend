using CleanArchitecture.Application.DTOs.CategoryDto;

namespace CleanArchitecture.Presentation.Endpoints;

public class CategoryController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/category").WithTags("Categories Management");

    #region Get Categories API
    group.MapGet("/", async (ICategoryService service) =>
    {
      var result = await service.GetAllCategoriesAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<CategoryResponse>>.SuccessResponse(result.Data!, "Retrieved Categories Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetCategories")
    .Produces<ApiResponse<List<CategoryResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetCategories")
    .WithDescription("Get Categories");
    #endregion
  }
}
