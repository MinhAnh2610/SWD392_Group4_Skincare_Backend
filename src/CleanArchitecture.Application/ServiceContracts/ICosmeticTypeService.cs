using CleanArchitecture.Application.DTOs.CosmeticTypeDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ICosmeticTypeService
{
  Task<Result<List<CosmeticTypeResponse>>> GetCosmeticTypesAsync();
}
