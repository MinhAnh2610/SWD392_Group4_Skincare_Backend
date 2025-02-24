using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.Strategies.BlogFilterStrategy
{
  public class ContentFilterStrategy : IBlogFilterStrategy
  {
    public IQueryable<Blog> ApplyFilter(IQueryable<Blog> query, GetProductRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Content))
      {
        return query;
      }
      return query.Where(blog => blog.Content.ToLower().Contains(request.Content.ToLower()));
    }
  }
}