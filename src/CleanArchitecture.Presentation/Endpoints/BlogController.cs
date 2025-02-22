using Carter.ModelBinding;
using CleanArchitecture.Application.DTOs.BlogDto;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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
    .Produces<ApiResponse<List<BlogResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetBlogs")
    .WithDescription("Get Blogs");
    #endregion

    #region Create Blog API

    app.MapPost("/", async (HttpContext context, IBlogService service, CreateBlogRequest createRequest) =>
    {
      await service.CreatePostAsync(createRequest);
    });

    #endregion
    
    #region Get Blog API
    app.MapGet("/{id}", async (IBlogService service, [FromRoute] Guid id) =>
    {
      var result = await service.GetBlogByIdAsync(id); 
      return result.Match(Message.SUCCESSFUL_RETRIEVED("Blog"));
    });
    #endregion
  }
}
