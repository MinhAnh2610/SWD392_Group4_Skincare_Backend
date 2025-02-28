using CleanArchitecture.Application.DTOs.BatchDto;
using CleanArchitecture.Application.DTOs.BrandDto;
using CleanArchitecture.Application.DTOs.CosmeticImageDto;
using CleanArchitecture.Application.DTOs.CosmeticSubcategory;
using CleanArchitecture.Application.DTOs.CosmeticTypeDto;
using CleanArchitecture.Application.DTOs.FeedbackDto;
using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.DTOs.RoutineStepDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using System.Text.Json.Serialization;
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
    public int Weigth { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VolumeUnit VolumeUnit { get; set; }
    public List<CosmeticSubcategoryResponse>? CosmeticSubcategories { get; set; }
    public List<CosmeticImageResponse>?CosmeticImages { get; set; }
  //  public List<CartItemResponse>? CartItems { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public List<BatchResponse>? Batches { get; set; }
    public List<FeedbackResponse>? Feedbacks { get; set; }
    public int Quantity =>
    Batches?
        .Where(b => b.ExpirationDate > DateOnly.FromDateTime(DateTime.Now))
        .Sum(b => b.Quantity) ?? 0;

    public decimal? Rating =>
        Feedbacks?.Count > 0
            ? Feedbacks.Average(f => f.Rating)
            : 0;

  }
}
