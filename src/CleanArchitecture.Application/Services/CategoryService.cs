using CleanArchitecture.Application.DTOs.CategoryDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class CategoryService : ICategoryService
{
  private readonly IUnitOfWork _unitOfWork;
  public CategoryService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<List<CategoryResponse>>> GetAllCategoriesAsync()
  {
    var result = await _unitOfWork.Categories.GetCategoriesAsync();

    return Result<List<CategoryResponse>>.Success(result.Select(c => new CategoryResponse
    {
      Id = c.Id,
      Name = c.Name,
      Description = c.Description,
    }).ToList(), StatusCodes.Status200OK);
  }
}
