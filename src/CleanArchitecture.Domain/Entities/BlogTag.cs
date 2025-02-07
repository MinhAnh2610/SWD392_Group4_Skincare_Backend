namespace CleanArchitecture.Domain.Entities;

public class BlogTag
{
  public Guid BlogId { get; set; }
  public Blog Blog { get; set; } = default!;
  public Guid TagId { get; set; }
  public Tag Tag { get; set; } = default!;
}