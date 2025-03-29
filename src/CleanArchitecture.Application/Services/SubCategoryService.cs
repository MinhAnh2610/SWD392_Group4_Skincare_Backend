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
  public async Task<Result<List<SubCategoryResponse>>> GetAllSubCategoriesAsync()
  {
    var result = await _unitOfWork.SubCategories.GetAllAsync();

    return Result<List<SubCategoryResponse>>.Success(result.Select(sc => new SubCategoryResponse
    {
      Id = sc.Id,
      Name = sc.Name,
      Description = sc.Description,
      CategoryId = sc.CategoryId,
    }).ToList(), StatusCodes.Status200OK);
  }
}
