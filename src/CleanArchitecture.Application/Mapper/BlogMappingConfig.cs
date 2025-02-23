using CleanArchitecture.Application.DTOs.BlogDto;
using Mapster;

namespace CleanArchitecture.Application.Mapper
{
  public class BlogMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<Blog, BlogResponse>();
    }
  }
}