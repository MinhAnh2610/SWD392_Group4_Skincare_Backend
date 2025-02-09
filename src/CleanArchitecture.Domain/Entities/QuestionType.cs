using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class QuestionType : Entity<Guid>
{
    public string? Name { get; set; }
    public List<QuestionOption>? QuestionOptions { get; set; }
}