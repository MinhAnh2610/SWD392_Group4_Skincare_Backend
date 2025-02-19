using CleanArchitecture.Application.DTOs.BrandDto;
using CleanArchitecture.Application.DTOs.CosmeticDto;
using CleanArchitecture.Application.DTOs.CosmeticImageDto;
using CleanArchitecture.Application.DTOs.CosmeticTypeDto;
using CleanArchitecture.Application.DTOs.FeedbackDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using CleanArchitecture.Application.DTOs.SubCategoryDto;
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
      Brand = new BrandResponse()
      {
        Id = c.BrandId,
        Name = c.Brand.Name,
        Description = c.Brand.Description,
        WebsiteUrl = c.Brand.WebsiteUrl,
        LogoUrl = c.Brand.LogoUrl,
      },
      SkinType = new SkinTypeResponse
      {
        Id = c.SkinTypeId,
        Name = c.SkinType.Name,
        Description = c.SkinType.Description,
        IsDry = c.SkinType.IsDry,
        IsSensitive = c.SkinType.IsSensitive,
        IsWrinkle = c.SkinType.IsWrinkle,
      },
      CosmeticType = new CosmeticTypeResponse
      {
        Id = c.CosmeticTypeId,
        Name = c.CosmeticType.Name,
        Description = c.CosmeticType.Description,
      },
      Name = c.Name,
      Price = c.Price,
      Gender = c.Gender,
      Notice = c.Notice,
      Ingredients = c.Ingredients,
      MainUsage = c.MainUsage,
      Texture = c.Texture,
      Origin = c.Origin,
      Instructions = c.Instructions,
      CosmeticSubcategories = c.CosmeticSubcategories.Select(sc => new SubCategoryResponse
      {
        Id = sc.SubCategoryId,
        Name = sc.SubCategory.Name,
        Description = sc.SubCategory.Description,
      }).ToList(),
      CosmeticImages = c.CosmeticImages.Select(ci => new CosmeticImageResponse
      {
        Id = ci.Id,
        CosmeticId = ci.CosmeticId,
        CosmeticName = ci.Cosmetic.Name,
        ImageUrl = ci.ImageUrl
      }).ToList(),
      Feedbacks = c.Feedbacks.Select(f => new FeedbackResponse
      {
        Id = f.Id,
        CustomerId = f.CustomerId,
        CustomerName = f.Customer.UserName,
        Content = f.Content,
        Rating = f.Rating
      }).ToList(),
    }).ToList(), StatusCodes.Status200OK);
  }
}
