using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cosmetic
{
    public class GetCosmeticsRequest
    {
      public GetCosmeticsRequest(
        string? name,
        Guid? brandId,
        Guid? skinTypeId,
        Guid? cosmeticTypeId,
        bool? gender,
        string? sortColumn,
        string? sortOrder,
        decimal? minPrice,
        decimal? maxPrice,
        int pageIndex,
        int pageSize)
      {
        Name = name;
        BrandId = brandId;
        SkinTypeId = skinTypeId;
        CosmeticTypeId = cosmeticTypeId;
        Gender = gender;
        SortColumn = sortColumn;
        SortOrder = sortOrder;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        PageIndex = pageIndex;
        PageSize = pageSize;
      }

      public string? Name { get; set; }
      public Guid? BrandId { get; set; }
      public Guid? SkinTypeId { get; set; }
      public Guid? CosmeticTypeId { get; set; }
      public bool? Gender { get; set; }
      public string? SortColumn { get; set; }
      public string? SortOrder { get; set; }
      public decimal? MinPrice { get; set; }
      public decimal? MaxPrice { get; set; }
      public int PageIndex { get; set; }
      public int PageSize { get; set; }
    }
  }
