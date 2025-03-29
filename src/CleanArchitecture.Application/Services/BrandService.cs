using CleanArchitecture.Application.DTOs.BrandDto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Services;

public class BrandService : IBrandService
{
  private readonly IUnitOfWork _unitOfWork;
  public BrandService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<BrandResponse>>> GetAllBrandsAsync()
  {
    var result = await _unitOfWork.Brands.GetAllAsync();

    return Result<List<BrandResponse>>.Success(result.Select(b => new BrandResponse
    {
      Id = b.Id,
      Name = b.Name,
      Description = b.Description,
      WebsiteUrl = b.WebsiteUrl,
      LogoUrl = b.LogoUrl
    }).ToList(), StatusCodes.Status200OK);
  }
}
