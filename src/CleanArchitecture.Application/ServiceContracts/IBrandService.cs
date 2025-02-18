using CleanArchitecture.Application.DTOs.BrandDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IBrandService
{
  Task<Result<List<BrandResponse>>> GetBrandsAsync();
}
