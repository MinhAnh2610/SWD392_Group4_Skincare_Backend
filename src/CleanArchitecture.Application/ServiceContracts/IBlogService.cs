using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IBlogService
{
  Task<Result<List<BlogResponse>>> GetAllBlogsAsync();
  Task<Result<BlogResponse>> GetBlogByIdAsync(Guid blogId);
  Task<Result<BlogResponse>> CreatePostAsync(CreateBlogRequest request);
}
