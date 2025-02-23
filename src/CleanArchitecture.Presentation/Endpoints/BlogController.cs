using CleanArchitecture.Application.DTOs.BlogDto;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints;

public class BlogController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/blogs").WithTags("Blogs Management");

    #region Get Blogs API
    group.MapGet("/", async (IBlogService service, string? title, string? content, string[]? tags, string? staffUsername, string? sortOrder = "asc", int pageIndex = 1, int pageSize = 10) =>
    {
      var request = new GetProductRequest(title, content, sortOrder, tags, staffUsername, pageIndex, pageSize);
      var result = await service.GetBlogsAsync(request);
      return result.Match(Message.SUCCESSFUL_RETRIEVED("Blogs"));
    })
    .WithName("GetBlogs")
    .Produces<ApiResponse<List<BlogResponse>>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetBlogs")
    .WithDescription("Get Blogs");
    #endregion

    #region Create Blog API

    group.MapPost("/", async (IBlogService service, CreateBlogRequest createRequest) =>
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
    group.MapGet("/{id}", async (IBlogService service, [FromRoute] Guid id) =>
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
    group.MapPut("/{id}", async (IBlogService service, [FromRoute] Guid id, [FromBody] UpdateBlogRequest updateRequest) =>
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
    group.MapDelete("/{id}", async (IBlogService service, [FromRoute] Guid id) =>
    {
      var result = await service.DeleteBlogAsync(id);
      return result.Match(Message.SUCCESSFUL_DELETED("Blog"));
    })
    .WithName("DeleteBlogById")
    .Produces<ApiResponse<BlogResponse>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("DeleteABlogById")
    .WithDescription("Delete a Blog By Id");
    #endregion
  }
}
