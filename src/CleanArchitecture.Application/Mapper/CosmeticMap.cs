using Mapster;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.DTOs.Cosmetic;

public class CosmeticMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    // Configure the mapping from Cosmetic to CosmeticResponse
    config.NewConfig<Cosmetic, CosmeticResponse>()
        .Map(dest => dest.Brand, src => src.Brand) // Map nested Brand
        .Map(dest => dest.SkinType, src => src.SkinType) // Map nested SkinType
        .Map(dest => dest.CosmeticType, src => src.CosmeticType) // Map nested CosmeticType
        .Map(dest => dest.CosmeticSubcategories, src => src.CosmeticSubcategories) // Map nested CosmeticSubcategories
        .Map(dest => dest.CosmeticImages, src => src.CosmeticImages) // Map nested CosmeticImages
        .Map(dest => dest.Feedbacks, src => src.Feedbacks); // Map nested Feedbacks
  }
}