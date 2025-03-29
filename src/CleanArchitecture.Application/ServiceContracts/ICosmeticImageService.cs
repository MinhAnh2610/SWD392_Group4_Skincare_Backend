using CleanArchitecture.Application.DTOs.CosmeticImageDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ICosmeticImageService
{
  Task<Result<List<CosmeticImageResponse>>> GetAllCosmeticImagesAsync();
}
