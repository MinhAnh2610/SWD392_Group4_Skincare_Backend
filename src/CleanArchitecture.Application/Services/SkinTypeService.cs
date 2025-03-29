using CleanArchitecture.Application.DTOs.SkinTypeDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class SkinTypeService : ISkinTypeService
{
  private readonly IUnitOfWork _unitOfWork;
  public SkinTypeService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<SkinTypeResponse>>> GetAllSkinTypesAsync()
  {
    var result = await _unitOfWork.SkinTypes.GetAllAsync();

    return Result<List<SkinTypeResponse>>.Success(result.Select(st => new SkinTypeResponse
    {
      Id = st.Id,
      Name = st.Name,
      Description = st.Description, 
      IsDry = st.IsDry,
      IsSensitive = st.IsSensitive,
      IsUneven = st.IsUneven,
      IsWrinkle = st.IsWrinkle  
    }).ToList(), StatusCodes.Status200OK);
  }
}
