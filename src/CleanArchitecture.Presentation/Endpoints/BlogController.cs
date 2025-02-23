using CleanArchitecture.Application.DTOs.BlogDto;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints;

public class BlogController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/blogs").WithTags("Blogs Management");

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
    .Produces<ApiResponse<List<BlogResponse>>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetBlogs")
    .WithDescription("Get Blogs");
    #endregion

    #region Create Blog API

    app.MapPost("/", async (IBlogService service, CreateBlogRequest createRequest) =>
    {
      var result = await service.CreateBlogAsync(createRequest);
      return result.Match(Message.SUCCESSFUL_CREATED("Blog"));
    })
    .WithName("CreateBlog")
    .Produces<ApiResponse<BlogResponse>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CreateBlogs")
    .WithDescription("Create a Blog");
    #endregion
    
    #region Get Blog API
    app.MapGet("/{id}", async (IBlogService service, [FromRoute] Guid id) =>
    {
      var result = await service.GetBlogByIdAsync(id); 
      return result.Match(Message.SUCCESSFUL_RETRIEVED("Blog"));
    })
    .WithName("GetBlogById")
    .Produces<ApiResponse<BlogResponse>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetABlogById")
    .WithDescription("Get a Blog By Id");
    #endregion
    
    #region Update Blog API
    app.MapPut("/{id}", async (IBlogService service, [FromRoute] Guid id, [FromBody] UpdateBlogRequest updateRequest) =>
    {
      var result = await service.UpdateBlogAsync(id, updateRequest);
      return result.Match(Message.SUCCESSFUL_UPDATED("Blog"));
    })
    .WithName("UpdateBlogById")
    .Produces<ApiResponse<BlogResponse>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("UpdateABlogById")
    .WithDescription("Update a Blog By Id");
    #endregion
    
    #region Delete Blog API
    app.MapDelete("/{id}", async (IBlogService service, [FromRoute] Guid id) =>
    {
      var result = await service.DeleteBlogAsync(id);
      return result.Match(Message.SUCCESSFUL_DELETED("Blog"));
    })
    .WithName("UpdateBlogById")
    .Produces<ApiResponse<BlogResponse>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("UpdateABlogById")
    .WithDescription("Update a Blog By Id");
    #endregion
  }
}
