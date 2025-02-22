using CleanArchitecture.Application.DTOs.CategoryDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ICategoryService
{
  Task<Result<List<CategoryResponse>>> GetAllCategoriesAsync();
  Task<Result<CategoryResponse>> CreateCategoryAsync(CreateCategoryRequest request);
  Task<Result<CategoryResponse>> UpdateCategoryAsync(UpdateCategoryRequest request);
  Task<Result<CategoryResponse>> DeleteCategory(Guid id);
}
