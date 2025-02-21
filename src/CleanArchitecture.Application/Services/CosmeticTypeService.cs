using CleanArchitecture.Application.DTOs.CosmeticTypeDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class CosmeticTypeService : ICosmeticTypeService
{
  private readonly IUnitOfWork _unitOfWork;
  public CosmeticTypeService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<CosmeticTypeResponse>>> GetAllCosmeticTypesAsync()
  {
    var result = await _unitOfWork.CosmeticTypes.GetAllAsync();

    return Result<List<CosmeticTypeResponse>>.Success(result.Select(ct => new CosmeticTypeResponse
    {
      Id = ct.Id,
      Name = ct.Name,
      Description = ct.Description,
    }).ToList(), StatusCodes.Status200OK);
  }
}
