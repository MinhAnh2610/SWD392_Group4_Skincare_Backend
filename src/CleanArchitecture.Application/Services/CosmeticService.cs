using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class CosmeticService : ICosmeticService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IErrorFactory _errorFactory;

    public CosmeticService(
      IUnitOfWork unitOfWork,
      IErrorFactory errorFactory
    )
    {
      _unitOfWork = unitOfWork;
      _errorFactory = errorFactory;
    }

    public async Task<Result<CosmeticResponse>> CreateCosmetic(CreateCosmetic cosmetic)
    {
      var orgcosmetic = cosmetic.Adapt<Cosmetic>();
      orgcosmetic.BrandId = cosmetic.BrandId;
      orgcosmetic.SkinTypeId = cosmetic.SkinTypeId;
      orgcosmetic.CosmeticTypeId = cosmetic.CosmeticTypeId;

      // Attach existing related entities to avoid re-adding them
      orgcosmetic.Brand = new Brand { Id = cosmetic.BrandId };
      orgcosmetic.SkinType = new SkinType { Id = cosmetic.SkinTypeId };
      orgcosmetic.CosmeticType = new CosmeticType { Id = cosmetic.CosmeticTypeId };

      _unitOfWork.Brands.Attach(orgcosmetic.Brand);
      _unitOfWork.SkinTypes.Attach(orgcosmetic.SkinType);
      _unitOfWork.CosmeticTypes.Attach(orgcosmetic.CosmeticType);

      //What to bind Cossubcate and feedbacks ?
      await _unitOfWork.Cosmetics.CreateAsync(orgcosmetic);
      var output = orgcosmetic.Adapt<CosmeticResponse>();
      return Result<CosmeticResponse>.Success(output, StatusCodes.Status201Created);
    }

    public async Task<Result<List<CosmeticResponse>>> GetAllCosmetics()
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetAllAsync();
      if (cosmetic != null)
      {
        var cosmeticsReponse = cosmetic.Adapt<List<CosmeticResponse>>();

        return Result<List<CosmeticResponse>>.Success(cosmeticsReponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<CosmeticResponse>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<CosmeticResponse>> GetCosmeticById(Guid id)
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(id);
      if (cosmetic != null)
      {
        var cosmeticResponse = cosmetic.Adapt<CosmeticResponse>();
        return Result<CosmeticResponse>.Success(cosmeticResponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<CosmeticResponse>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<List<CosmeticResponse>>> GetCosmeticsByBrandId(Guid brandId)
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetListByAnyId(e => e.BrandId == brandId);
      if (cosmetic != null)
      {
        var cosmeticResponse = cosmetic.Adapt<List<CosmeticResponse>>();
        return Result<List<CosmeticResponse>>.Success(cosmeticResponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<CosmeticResponse>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<List<CosmeticResponse>>> GetCosmeticsByName(string name)
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetListByAnyId(e => e.Name == name);
      if (cosmetic != null)
      {
        var cosmeticResponse = cosmetic.Adapt<List<CosmeticResponse>>();
        return Result<List<CosmeticResponse>>.Success(cosmeticResponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<CosmeticResponse>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<List<CosmeticResponse>>> GetCosmeticsBySkinTypeId(Guid skinTypeId)
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetListByAnyId(e => e.SkinTypeId == skinTypeId);
      if (cosmetic != null)
      {
        var cosmeticResponse = cosmetic.Adapt<List<CosmeticResponse>>();
        return Result<List<CosmeticResponse>>.Success(cosmeticResponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<CosmeticResponse>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<List<CosmeticResponse>>> GetCosmeticsByTypeId(Guid typeId)
    {
      var cosmetic = await _unitOfWork.Cosmetics.GetListByAnyId(e => e.CosmeticTypeId == typeId);
      if (cosmetic != null)
      {
        var cosmeticResponse = cosmetic.Adapt<List<CosmeticResponse>>();
        return Result<List<CosmeticResponse>>.Success(cosmeticResponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<CosmeticResponse>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<CosmeticResponse>> UpdateCosmetic(UpdateCosmetic cosmetic, Guid id)
    {
      var existcosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(id);
      if (existcosmetic == null)
      {
        return Result<CosmeticResponse>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }

      // Only update if the new value is NOT null
      existcosmetic.Price = cosmetic.Price != default ? cosmetic.Price : existcosmetic.Price;
      existcosmetic.MainUsage =
        !string.IsNullOrWhiteSpace(cosmetic.MainUsage) ? cosmetic.MainUsage : existcosmetic.MainUsage;
      existcosmetic.Instructions = !string.IsNullOrWhiteSpace(cosmetic.Instructions)
        ? cosmetic.Instructions
        : existcosmetic.Instructions;
      existcosmetic.LastModified = DateTime.Now;

      _unitOfWork.Cosmetics.Update(existcosmetic);
      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Cosmetic");
        return Result<CosmeticResponse>.Failure([error.err], error.statusCode);
      }

      var output = existcosmetic.Adapt<CosmeticResponse>();

      return Result<CosmeticResponse>.Success(output, StatusCodes.Status200OK);
    }

    public async Task<Result<CosmeticResponse>> DeleteCosmetic(Guid id)
    {
      var existcosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(id);
      if (existcosmetic == null)
      {
        return Result<CosmeticResponse>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }

      _unitOfWork.Cosmetics.Remove(existcosmetic);
      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Cosmetic");
        return Result<CosmeticResponse>.Failure([error.err], error.statusCode);
      }
      var output = existcosmetic.Adapt<CosmeticResponse>();
      return Result<CosmeticResponse>.Success(output, StatusCodes.Status200OK);
    }

    public async Task<Result<List<CosmeticResponse>>> SearchCosmetics(FilterCosmeticRequest filter)
    {
      var query = await _unitOfWork.Cosmetics.GetAllAsync();

      if (query == null)
      {
        return Result<List<CosmeticResponse>>.Failure(
          [CosmeticErrors.CosmeticNotFound],
          StatusCodes.Status404NotFound
        );
      }

      // Apply filters if they exist
      var filteredResults = query.Where(c =>
        // Name filter (case-insensitive contains)
        (string.IsNullOrEmpty(filter.Name) || c.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase)) &&
        // Type filter
        (!filter.TypeId.HasValue || c.CosmeticTypeId == filter.TypeId) &&
        // Brand filter
        (!filter.BrandId.HasValue || c.BrandId == filter.BrandId) &&
        // Skin type filter
        (!filter.SkinTypeId.HasValue || c.SkinTypeId == filter.SkinTypeId)
      ).ToList();

      if (filteredResults?.Any() != true)
      {
        return Result<List<CosmeticResponse>>.Failure(
          [CosmeticErrors.CosmeticNotFound],
          StatusCodes.Status404NotFound
        );
      }

      var cosmeticResponse = filteredResults.Adapt<List<CosmeticResponse>>();
      return Result<List<CosmeticResponse>>.Success(
        cosmeticResponse,
        StatusCodes.Status200OK
      );
    }
  }
}