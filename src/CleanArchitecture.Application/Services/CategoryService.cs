using CleanArchitecture.Application.DTOs.CategoryDto;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class CategoryService : ICategoryService
{
  private readonly IUnitOfWork _unitOfWork;
  public CategoryService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CategoryResponse>> CreateCategoryAsync(CreateCategoryRequest request)
  {
    var existingCategory = await _unitOfWork.Categories.GetByIdAsync(request.Id);
    if(existingCategory != null)
    {
      return Result<CategoryResponse>.Failure([CategoryErrors.CategoryAlreadyExist], StatusCodes.Status404NotFound);
    }
    var category = new Category
    {
      Id = request.Id,
      Name = request.Name,
      Description = request.Description,
    };
    var createdCategory = await _unitOfWork.Categories.CreateAsync(category);
    
    return Result<CategoryResponse>.Success(createdCategory.Adapt<CategoryResponse>(), StatusCodes.Status201Created);

  }

  public async Task<Result<CategoryResponse>> DeleteCategory(Guid id)
  {
    var category = await _unitOfWork.Categories.GetByIdAsync(id);
    if(category == null) 
    {
      return Result<CategoryResponse>.Failure([CategoryErrors.CategoryNotFound], StatusCodes.Status404NotFound);
    }
    var deletedCategory = await _unitOfWork.Categories.RemoveAsync(category);
    if(!deletedCategory)
    {
      return Result<CategoryResponse>.Failure([CategoryErrors.CategoryNotFound], StatusCodes.Status404NotFound);
    }
    return Result<CategoryResponse>.Success(category.Adapt<CategoryResponse>(), StatusCodes.Status200OK);
  }

  public async Task<Result<List<CategoryResponse>>> GetAllCategoriesAsync()
  {
    var result = await _unitOfWork.Categories.GetAllAsync();

    return Result<List<CategoryResponse>>.Success(result.Select(c => new CategoryResponse
    {
      Id = c.Id,
      Name = c.Name,
      Description = c.Description,
    }).ToList(), StatusCodes.Status200OK);
  }

  public async Task<Result<CategoryResponse>> UpdateCategoryAsync(UpdateCategoryRequest request)
  {
    var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
    if(category == null)
    {
      return Result<CategoryResponse>.Failure([CategoryErrors.CategoryNotFound], StatusCodes.Status404NotFound);
    }
    if(!string.IsNullOrEmpty(request.Name))
    {
      category.Name = request.Name;
    }
    if(!string.IsNullOrEmpty(request.Description))
    {
       category.Description = request.Description;
    }
    await _unitOfWork.Categories.UpdateAsync(category);
    return Result<CategoryResponse>.Success(category.Adapt<CategoryResponse>(), StatusCodes.Status200OK);

  }
}
