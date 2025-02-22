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

    #region Create Category API
    group.MapPost("/create", async (ICategoryService service, CreateCategoryRequest request) =>
    {
      var result = await service.CreateCategoryAsync(request);
      if (result != null)
      {
        return Results.Ok(ApiResponse<CategoryResponse>.SuccessResponse(result.Data!, "Create Categories Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
      .WithName("CreateCategories")
      .Produces<ApiResponse<CategoryResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("CreateCategories")
      .WithDescription("Create Categories");
    #endregion

    #region Delete Category API
    group.MapDelete("/delete/{id}", async (ICategoryService service, Guid id) =>
    {
      var result = await service.DeleteCategory(id);
      if(result != null)
      {
        return Results.Ok(ApiResponse<CategoryResponse>.SuccessResponse(result.Data!, "Delete Categories Successfully."));
      }
      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
      .WithName("DeleteCategories")
      .Produces<ApiResponse<CategoryResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("DeleteCategories")
      .WithDescription("Delete Categories");
    #endregion

    #region Update Category API
    group.MapPut("/update/{id}", async (ICategoryService service, UpdateCategoryRequest request) =>
    {
      var result = await service.UpdateCategoryAsync(request);
      if(result != null)
      {
        return Results.Ok(ApiResponse<CategoryResponse>.SuccessResponse(result.Data!, "Update Categories Successfully."));
      }
      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
      .WithName("UpdateCategories")
      .Produces<ApiResponse<CategoryResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("UpdateCategories")
      .WithDescription("Update Categories");
    #endregion


  }


}
