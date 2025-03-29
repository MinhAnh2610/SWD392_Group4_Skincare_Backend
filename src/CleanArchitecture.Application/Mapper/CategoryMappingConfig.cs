using Mapster;
using CleanArchitecture.Application.DTOs.Category;
namespace CleanArchitecture.Application.Mapper
{
  public class CategoryMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      // Map Category → CategoryResponse
      config.NewConfig<Category, CategoryResponse>()
      .Map(dest => dest.Subcategories, src => src.SubCategories);

      // Map CategoryRequest → Category (for creating/updating)
      config.NewConfig<UpdateCategory, CategoryResponse>();
    }
  }
}