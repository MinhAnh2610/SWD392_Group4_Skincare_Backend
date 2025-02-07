using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Blog : Entity<Guid>
{
    public Guid StaffId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}