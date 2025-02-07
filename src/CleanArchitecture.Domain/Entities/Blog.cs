namespace CleanArchitecture.Domain.Entities;

public class Blog : Entity<Guid>
{
  public Guid StaffId { get; set; }
  public User Staff { get; set; } = default!;
  public string? Title { get; set; }
  public string? Content { get; set; }
  public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
}