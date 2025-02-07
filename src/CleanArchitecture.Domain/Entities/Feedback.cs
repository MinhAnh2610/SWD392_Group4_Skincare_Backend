using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Feedback : Entity<Guid>
{
    public Guid CosmeticId { get; set; }
    public Guid CustomerId { get; set; }
    public string? Content { get; set; }
    public decimal Rating { get; set; }
}