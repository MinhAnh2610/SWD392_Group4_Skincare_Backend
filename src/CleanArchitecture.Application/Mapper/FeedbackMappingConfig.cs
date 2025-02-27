using CleanArchitecture.Application.DTOs.FeedbackDto;
using Mapster;

namespace CleanArchitecture.Application.Mapper;

public class FeedbackMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<Feedback, FeedbackResponse>()
            .Map(dest => dest.CustomerName, src => src.Customer.UserName);
  }
}
