using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IBlogService
{ 
  Task<Result<List<BlogResponse>>> GetAllBlogsAsync();
  Task<Result<PaginatedList<BlogResponse>>> GetBlogsAsync(GetProductRequest request);
  Task<Result<BlogResponse>> GetBlogByIdAsync(Guid blogId);
  Task<Result<BlogResponse>> CreateBlogAsync(CreateBlogRequest request);
  Task<Result<BlogResponse>> UpdateBlogAsync(Guid id, UpdateBlogRequest request);
  Task<Result<BlogResponse>> DeleteBlogAsync(Guid id);
}
