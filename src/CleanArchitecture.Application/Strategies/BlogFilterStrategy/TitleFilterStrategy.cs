using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.Strategies.BlogFilterStrategy
{
  public class TitleFilterStrategy : IBlogFilterStrategy
  {
    public IQueryable<Blog> ApplyFilter(IQueryable<Blog> query, GetProductRequest request)
    {
      if (string.IsNullOrEmpty(request.Title))
      {
        return query;
      }
      return query.Where(blog => blog.Title.ToLower().Contains(request.Title.ToLower()));
    }
  }
}