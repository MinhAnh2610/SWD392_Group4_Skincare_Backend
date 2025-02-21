using CleanArchitecture.Application.DTOs.CosmeticDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ICosmeticService
{
  Task<Result<List<CosmeticResponse>>> GetAllCosmeticsAsync();
}
