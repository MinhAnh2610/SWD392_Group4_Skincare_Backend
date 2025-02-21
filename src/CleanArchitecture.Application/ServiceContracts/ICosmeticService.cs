using CleanArchitecture.Application.DTOs.Cosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
public interface ICosmeticService
{
    Task<Result<List<CosmeticResponse>>> GetAllCosmetics();
    Task<Result<CosmeticResponse>> GetCosmeticById(Guid id);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsByBrandId(Guid brandId);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsByTypeId(Guid typeId);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsBySkinTypeId(Guid skinTypeId);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsByName(string name);
    Task<Result<CreateCosmetic>> CreateCosmetic(Cosmetic cosmetic);
    Task<Result<CosmeticResponse>> UpdateCosmetic(UpdateCosmetic cosmetic);
    Task<Result<CosmeticResponse>> DeleteCosmetic(Guid id);

    Task<Result<List<CosmeticResponse>>> SearchCosmetics(FilterCosmeticRequest filter);
  }
}
