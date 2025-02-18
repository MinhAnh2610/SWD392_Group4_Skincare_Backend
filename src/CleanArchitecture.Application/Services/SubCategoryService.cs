using CleanArchitecture.Application.DTOs.CategoryDto;
using CleanArchitecture.Application.DTOs.SubCategoryDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class SubCategoryService : ISubCategoryService
{
  private readonly IUnitOfWork _unitOfWork;
  public SubCategoryService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<SubCategoryResponse>>> GetSubCategoriesAsync()
  {
    var result = await _unitOfWork.SubCategories.GetAllSubCategoriesAsync();

    return Result<List<SubCategoryResponse>>.Success(result.Select(sc => new SubCategoryResponse
    {
      Id = sc.Id,
      Name = sc.Name,
      Description = sc.Description,
      CategoryName = sc.Category.Name,
    }).ToList(), StatusCodes.Status200OK);
  }
}
