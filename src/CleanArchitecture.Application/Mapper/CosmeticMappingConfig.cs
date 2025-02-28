using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.DTOs.CosmeticImageDto;
using CleanArchitecture.Application.DTOs.FeedbackDto;
using Mapster;
namespace CleanArchitecture.Application.Mapper
{
  public class CosmeticMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      // Configure the mapping from Cosmetic to CosmeticResponse
      config.NewConfig<Cosmetic, CosmeticResponse>()
        .Map(dest => dest.Feedbacks, src => src.Feedbacks.Adapt<List<FeedbackResponse>>());
      config.NewConfig<UpdateCosmetic, CosmeticResponse>();
      config.NewConfig<CreateCosmetic, CosmeticResponse>();
      config.NewConfig<Cosmetic, CreateCosmetic>();


    }
  }
}
