using CleanArchitecture.Application.DTOs.CosmeticImageDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class CosmeticImageService : ICosmeticImageService
{
  private readonly IUnitOfWork _unitOfWork;
  public CosmeticImageService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<CosmeticImageResponse>>> GetAllCosmeticImagesAsync()
  {
    var result = await _unitOfWork.CosmeticImages.GetAllAsync();

    return Result<List<CosmeticImageResponse>>.Success(result.Select(ci => new CosmeticImageResponse
    {
      Id = ci.Id,
      CosmeticId = ci.Id,
      CosmeticName = ci.Cosmetic.Name,
      ImageUrl = ci.ImageUrl
    }).ToList(), StatusCodes.Status200OK);
  }
}
