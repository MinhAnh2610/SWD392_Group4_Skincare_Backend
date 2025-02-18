using CleanArchitecture.Application.DTOs.SkinTypeDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ISkinTypeService
{
  Task<Result<List<SkinTypeResponse>>> GetSkinTypesAsync();
}
