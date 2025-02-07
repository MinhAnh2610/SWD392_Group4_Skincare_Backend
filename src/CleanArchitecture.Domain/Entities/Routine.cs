using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Routine : Entity<Guid>
{
    public Guid SkinTypeId { get; set; }
    public string? Title { get; set; }
    public string? Period { get; set; }
}