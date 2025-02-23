using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.Strategies.BlogFilterStrategy
{
  public interface IBlogFilterStrategy
  {
    IQueryable<Blog> ApplyFilter(IQueryable<Blog> query, GetProductRequest request);
  }
}