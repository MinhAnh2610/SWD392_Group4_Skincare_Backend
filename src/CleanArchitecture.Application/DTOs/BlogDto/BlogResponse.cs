using CleanArchitecture.Application.DTOs.TagDto;

namespace CleanArchitecture.Application.DTOs.BlogDto;

public class BlogResponse
{
  public Guid Id { get; set; }
  public string? StaffName { get; set; }
  public string? Title { get; set; }
  public string? ShortenContent { get; set; }
  public string? Content { get; set; }
  public List<TagResponse>? Tags { get; set; }
}
