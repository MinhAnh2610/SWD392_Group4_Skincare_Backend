using Mapster;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.DTOs.SubCategoryDto;
namespace CleanArchitecture.Application.Mapper
{
  public class CosmeticMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      // Configure the mapping from Cosmetic to CosmeticResponse
      config.NewConfig<Cosmetic, CosmeticResponse>();
      config.NewConfig<UpdateCosmetic, CosmeticResponse>();
      config.NewConfig<CreateCosmetic, CosmeticResponse>();
      config.NewConfig<Cosmetic, CreateCosmetic>();
    }
  }
}
