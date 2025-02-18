using CleanArchitecture.Application.DTOs.Cosmetic;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class CosmeticService : ICosmeticService
{
  private readonly IUnitOfWork _unitOfWork;

  public CosmeticService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<List<CosmeticResponse>>> GetAllCosmeticsAsync()
  {
    var cosmetics = await _unitOfWork.Cosmetics.GetAllAsync();

    return Result<List<CosmeticResponse>>.Success(cosmetics.Select(c => new CosmeticResponse
    {
      Id = c.Id,
      CreateAt = c.CreateAt,
      CreatedBy = c.CreatedBy,
      LastModified = c.LastModified,
      LastModifiedBy = c.LastModifiedBy,
      IsActive = c.IsActive,
      BrandId = c.BrandId,
      Brand = c.Brand,
      SkinTypeId = c.SkinTypeId,
      SkinType = c.SkinType,
      CosmeticTypeId = c.CosmeticTypeId,
      CosmeticType = c.CosmeticType,
      Name = c.Name,
      Price = c.Price,
      Gender = c.Gender,
      Notice = c.Notice,
      Ingredients = c.Ingredients,
      MainUsage = c.MainUsage,
      Texture = c.Texture,
      Origin = c.Origin,
      Instructions = c.Instructions,
      CosmeticSubcategories = c.CosmeticSubcategories.ToList(),
      CosmeticImages = c.CosmeticImages.ToList(),
      Feedbacks = c.Feedbacks.ToList()
    }).ToList(), StatusCodes.Status200OK);
  }
}
