namespace CleanArchitecture.Application.DTOs.SubCategoryDto;

public class SubCategoryResponse
{
  public Guid Id { get; set; }
  public string CategoryName { get; set; } = default!;
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
}
