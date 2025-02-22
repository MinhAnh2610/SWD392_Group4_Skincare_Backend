using CleanArchitecture.Application.DTOs.BlogDto;
using CleanArchitecture.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class BlogService : IBlogService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  public BlogService(IUnitOfWork unitOfWork, IErrorFactory errorFactory)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
  }

  public async Task<Result<List<BlogResponse>>> GetAllBlogsAsync()
  {
    var blogs = await _unitOfWork.Blogs.GetAllAsync();

    return Result<List<BlogResponse>>.Success(blogs.Select(b => new BlogResponse
    {
      Id = b.Id,
      StaffId = b.StaffId,
      Staff = b.Staff,
      Title = b.Title,
      Content = b.Content,
      BlogTags = b.BlogTags.ToList(),
    }).ToList(), StatusCodes.Status200OK);
  }

  public async Task<Result<BlogResponse>> GetBlogByIdAsync(Guid blogId)
  {
    var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
    if (blog is null)
    {
      var error = _errorFactory.CreateNotFoundError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }

    return Result<BlogResponse>.Success(blog.Adapt<BlogResponse>(), StatusCodes.Status200OK);
  }

  public Task<Result<BlogResponse>> CreatePostAsync(CreateBlogRequest request)
  {
    throw new NotImplementedException();
  }
}
