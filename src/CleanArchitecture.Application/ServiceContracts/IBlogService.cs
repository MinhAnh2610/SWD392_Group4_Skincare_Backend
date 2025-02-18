using CleanArchitecture.Application.DTOs.Blog;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IBlogService
{
  Task<Result<List<BlogResponse>>> GetAllBlogsAsync();
}
