using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class QuestionOption : Entity<Guid>
{
    public Guid QuestionId { get; set; }
    public Guid QuestionTypeId { get; set; }
}