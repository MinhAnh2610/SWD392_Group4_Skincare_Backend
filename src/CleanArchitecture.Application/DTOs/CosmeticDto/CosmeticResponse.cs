using CleanArchitecture.Application.DTOs.BrandDto;
using CleanArchitecture.Application.DTOs.CosmeticImageDto;
using CleanArchitecture.Application.DTOs.CosmeticTypeDto;
using CleanArchitecture.Application.DTOs.FeedbackDto;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using CleanArchitecture.Application.DTOs.SubCategoryDto;

namespace CleanArchitecture.Application.DTOs.CosmeticDto;

public class CosmeticResponse
{
  public Guid Id { get; set; }
  public BrandResponse? Brand { get; set; }
  public SkinTypeResponse? SkinType { get; set; }
  public CosmeticTypeResponse? CosmeticType { get; set; }
  public string Name { get; set; } = default!;
  public decimal Price { get; set; }
  public bool Gender { get; set; }
  public string Notice { get; set; } = default!;
  public string Ingredients { get; set; } = default!;
  public string MainUsage { get; set; } = default!;
  public string Texture { get; set; } = default!;
  public string Origin { get; set; } = default!;
  public string Instructions { get; set; } = default!;
  public List<SubCategoryResponse>? CosmeticSubcategories { get; set; }
  public List<CosmeticImageResponse>? CosmeticImages { get; set; }
  public List<FeedbackResponse>? Feedbacks { get; set; }
}
