using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Question : Entity<Guid>
{
    public Guid QuizId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Instruction { get; set; }
    public string? Section { get; set; }
    
    //Navigation Properties
    public Quiz? Quiz { get; set; }
}