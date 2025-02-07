using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Quiz : Entity<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int TargetAgeFrom { get; set; }
    public int TargetAgeTo { get; set; }
    
    // Navigation Properties
    public ICollection<Question>? Questions;
}