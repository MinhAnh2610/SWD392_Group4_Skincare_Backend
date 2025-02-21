using CleanArchitecture.Application.DTOs.CategoryDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ICategoryService
{
  Task<Result<List<CategoryResponse>>> GetAllCategoriesAsync();
}
