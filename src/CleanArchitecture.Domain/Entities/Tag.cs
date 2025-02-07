namespace CleanArchitecture.Domain.Entities;

public class Tag : Entity<Guid>
{
  public string? Name { get; set; }
  public string? Description { get; set; }
  public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
}