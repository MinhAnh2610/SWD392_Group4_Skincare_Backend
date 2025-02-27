using CleanArchitecture.Application.DTOs.BatchDto;
using CleanArchitecture.Application.DTOs.BrandDto;
using CleanArchitecture.Application.DTOs.CartItem;
using CleanArchitecture.Application.DTOs.CosmeticImageDto;
using CleanArchitecture.Application.DTOs.CosmeticSubcategory;
using CleanArchitecture.Application.DTOs.CosmeticTypeDto;
using CleanArchitecture.Application.DTOs.FeedbackDto;
using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.DTOs.RoutineStepDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using CleanArchitecture.Application.DTOs.SubCategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class CosmeticResponse
  {
    public Guid Id { get; set; }
    public DateTime? CreateAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public Guid BrandId { get; set; }
    public BrandResponse? Brand { get; set; }
    public Guid SkinTypeId { get; set; }
    public SkinTypeResponse? SkinType { get; set; }
    public Guid CosmeticTypeId { get; set; }
    public CosmeticTypeResponse? CosmeticType { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public bool Gender { get; set; } = true;
    public string Notice { get; set; } = default!;
    public string Ingredients { get; set; } = default!;
    public string MainUsage { get; set; } = default!;
    public string Texture { get; set; } = default!;
    public string Origin { get; set; } = default!;
    public string Instructions { get; set; } = default!;
    public ushort Size { get; set; } = default!;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VolumeUnit VolumeUnit { get; set; }
    public List<CosmeticSubcategoryResponse>? CosmeticSubcategories { get; set; }
    public List<CosmeticImageResponse>?CosmeticImages { get; set; }
  //  public List<CartItemResponse>? CartItems { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public List<BatchResponse>? Batches { get; set; }
    public List<RoutineStepResponse>? RoutineSteps { get; set; }
    public List<FeedbackResponse>? Feedbacks { get; set; }
    public List<RefundItemResponse>? RefundItems { get; set; }

  }
}
