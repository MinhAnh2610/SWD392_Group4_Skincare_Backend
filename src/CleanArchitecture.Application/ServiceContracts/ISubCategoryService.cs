using CleanArchitecture.Application.DTOs.SubCategoryDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ISubCategoryService
{
  Task<Result<List<SubCategoryResponse>>> GetAllSubCategoriesAsync();
}
