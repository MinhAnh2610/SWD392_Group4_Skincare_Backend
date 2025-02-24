using CleanArchitecture.Application.DTOs.BlogDto;

namespace CleanArchitecture.Application.Strategies.BlogFilterStrategy
{
  public class StaffUsernameFilterStrategy : IBlogFilterStrategy
  {
    public IQueryable<Blog> ApplyFilter(IQueryable<Blog> query, GetProductRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.StaffUsername))
      {
        return query;
      }
      
      return query.Where(b => b.Staff.UserName.ToLower().Contains(request.StaffUsername.ToLower())); 
    }
  }
}