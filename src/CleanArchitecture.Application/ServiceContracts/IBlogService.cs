using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IBlogService
{
  Task<Result<List<BlogResponse>>> GetAllBlogsAsync();
}
