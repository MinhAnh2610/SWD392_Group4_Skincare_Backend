using CleanArchitecture.Application.DTOs.BlogDto;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Strategies.BlogFilterStrategy
{
  public class SortOrderFilterStrategy : IBlogFilterStrategy
  {
    public IQueryable<Blog> ApplyFilter(IQueryable<Blog> query, GetProductRequest request)
    {
      if (string.IsNullOrEmpty(request.SortColumn))
      {
        return query;
      }
      
      query = request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(GetSortProperty(request)) : query.OrderBy(GetSortProperty(request));
      
      return query;
    }

    private Expression<Func<Blog, object>> GetSortProperty(GetProductRequest request)
    {
      Expression<Func<Blog, object>> keySelector = request.SortColumn?.ToLower() switch
      {
        "created_at" => blog => blog.CreateAt,
        "title" => blog => blog.Title,
        _ => blog => blog.CreateAt
      };
      
      return keySelector;
    }
  }
}