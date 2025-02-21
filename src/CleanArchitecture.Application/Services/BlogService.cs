using CleanArchitecture.Application.DTOs.BlogDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class BlogService : IBlogService
{
  private readonly IUnitOfWork _unitOfWork;
  public BlogService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
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
}
