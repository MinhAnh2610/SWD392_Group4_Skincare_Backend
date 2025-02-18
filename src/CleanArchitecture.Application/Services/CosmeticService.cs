using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArchitecture.Application.Services
{
public class CosmeticService : ICosmeticService
{
    private readonly ICosmeticRepository _cosmeticRepository;
   private readonly IUnitOfWork _unitOfWork;

        public CosmeticService(
        ICosmeticRepository cosmeticRepository
      )
    {
      _cosmeticRepository = cosmeticRepository;

    }
    public async Task<Result<CreateCosmetic>> CreateCosmetic(Cosmetic cosmetic)
    {
      throw new NotImplementedException();
    }

    public async Task<Result<List<CosmeticResponse>>> GetAllCosmetics()
    {
      var cosmetic = await _cosmeticRepository.GetAllAsync();
      if (cosmetic != null)
      {
        return Result<List<Cosmetic>>.Success(cosmetic, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<Cosmetic>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<Cosmetic>> GetCosmeticById(int id)
    {
        var cosmetic = await _cosmeticRepository.GetByIdAsync(id);
        if (cosmetic != null)
        {
          return Result<Cosmetic>.Success(cosmetic, StatusCodes.Status200OK);
        }
        else
        {
          return Result<Cosmetic>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
        }
    }

    public async Task<Result<List<Cosmetic>>> GetCosmeticsByBrandId(Guid brandId)
    {
      var cosmetic = await _cosmeticRepository.GetListByAnyId(e => e.BrandId == brandId);
      if (cosmetic != null)
      {
        return Result<List<Cosmetic>>.Success(cosmetic, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<Cosmetic>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }
    public async Task<Result<List<Cosmetic>>> GetCosmeticsByName(string name)
    {
      var cosmetic = await _cosmeticRepository.GetListByAnyId(e => e.Name == name);
      if (cosmetic != null)
      {
        return Result<List<Cosmetic>>.Success(cosmetic, StatusCodes.Status200OK);
      }
      else
  {
        return Result<List<Cosmetic>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
  }

    public async Task<Result<List<Cosmetic>>> GetCosmeticsByTypeId(Guid typeId)
    {
      var cosmetic = await _cosmeticRepository.GetListByAnyId(e => e.CosmeticTypeId == typeId);
      if (cosmetic != null)
      {
        return Result<List<Cosmetic>>.Success(cosmetic, StatusCodes.Status200OK);
      }
      else
  {
        return Result<List<Cosmetic>>.Failure([CosmeticErrors.CosmeticNotFound], StatusCodes.Status404NotFound);
      }
    }

    public Task<Result<UpdateCosmetic>> UpdateCosmetic(Cosmetic cosmetic)
    {
      throw new NotImplementedException();
    }
  }
    public async Task<Result<List<CosmeticResponse>>> GetAllCosmeticsAsync()
    {
        var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsAsync();

        return Result<List<CosmeticResponse>>.Success(cosmetics.Select(c => new CosmeticResponse
        {
            Id = c.Id,
            CreateAt = c.CreateAt,
            CreatedBy = c.CreatedBy,
            LastModified = c.LastModified,
            LastModifiedBy = c.LastModifiedBy,
            IsActive = c.IsActive,
            BrandId = c.BrandId,
            Brand = c.Brand,
            SkinTypeId = c.SkinTypeId,
            SkinType = c.SkinType,
            CosmeticTypeId = c.CosmeticTypeId,
            CosmeticType = c.CosmeticType,
            Name = c.Name,
            Price = c.Price,
            Gender = c.Gender,
            Notice = c.Notice,
            Ingredients = c.Ingredients,
            MainUsage = c.MainUsage,
            Texture = c.Texture,
            Origin = c.Origin,
            Instructions = c.Instructions,
            CosmeticSubcategories = c.CosmeticSubcategories.ToList(),
            CosmeticImages = c.CosmeticImages.ToList(),
            Feedbacks = c.Feedbacks.ToList()
        }).ToList(), StatusCodes.Status200OK);
    }
}
