using CleanArchitecture.Application.DTOs.Cosmetic;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ICosmeticService
{
  Task<Result<List<CosmeticResponse>>> GetAllCosmeticsAsync();
}
