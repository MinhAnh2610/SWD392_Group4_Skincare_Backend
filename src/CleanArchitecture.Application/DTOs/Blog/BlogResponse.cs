namespace CleanArchitecture.Application.DTOs.Blog;

public class BlogResponse
{
  public Guid Id { get; set; }
  public DateTime? CreateAt { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime? LastModified { get; set; }
  public string? LastModifiedBy { get; set; }
  public bool IsActive { get; set; }
  public Guid StaffId { get; set; }
  public Domain.Entities.User? Staff { get; set; }
  public string? Title { get; set; }
  public string? Content { get; set; }
  public List<BlogTag>? BlogTags { get; set; }
}
