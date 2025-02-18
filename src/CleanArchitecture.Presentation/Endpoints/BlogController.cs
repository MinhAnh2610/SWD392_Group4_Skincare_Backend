
using CleanArchitecture.Application.DTOs.Blog;
using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Presentation.Endpoints;

public class BlogController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/blog").WithTags("Blogs Management");

    #region Get Blogs API
    group.MapGet("/", async (IBlogService service) =>
    {
      var result = await service.GetAllBlogsAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<BlogResponse>>.SuccessResponse(result.Data!, "Retrieved Blogs Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetBlogs")
    .Produces<ApiResponse<CompanyInformation>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetBlogs")
    .WithDescription("Get Blogs");
    #endregion
  }
}
