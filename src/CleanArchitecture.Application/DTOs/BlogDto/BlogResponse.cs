namespace CleanArchitecture.Application.DTOs.BlogDto;

public class BlogResponse
{
  public Guid Id { get; set; }
  public Guid StaffId { get; set; }
  public User? Staff { get; set; }
  public string? Title { get; set; }
  public string? Content { get; set; }
  public List<BlogTag>? BlogTags { get; set; }
}
