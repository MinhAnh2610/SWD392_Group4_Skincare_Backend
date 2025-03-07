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
    Task<Result<PaginatedList<CosmeticResponse>>> GetCosmeticsAsync(GetCosmeticsRequest request);
    Task<Result<CosmeticResponse>> GetCosmeticById(Guid id);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsByBrandId(Guid brandId);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsByTypeId(Guid typeId);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsBySkinTypeId(Guid skinTypeId);
    Task<Result<List<CosmeticResponse>>> GetCosmeticsByName(string name);
    Task<Result<CosmeticResponse>> CreateCosmetic(CreateCosmetic cosmetic);
    Task<Result<CosmeticResponse>> UpdateCosmetic(UpdateCosmetic cosmetic,Guid id);
    Task<Result<CosmeticResponse>> DeleteCosmetic(Guid id);
    Task<Result<CosmeticResponse>> UploadCosmeticImages(CosmeticImagesUploadRequest request);

    Task<Result<List<CosmeticResponse>>> SearchCosmetics(FilterCosmeticRequest filter);
  }
}
