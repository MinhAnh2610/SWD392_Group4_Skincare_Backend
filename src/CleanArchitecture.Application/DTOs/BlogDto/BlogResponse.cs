namespace CleanArchitecture.Application.DTOs.BlogDto;

public class BlogResponse
{
  public Guid Id { get; set; }
  public string? StaffName { get; set; }
  public string? Title { get; set; }
  public string? ShortenContent { get; set; }
  public List<BlogTag>? BlogTags { get; set; }
}
