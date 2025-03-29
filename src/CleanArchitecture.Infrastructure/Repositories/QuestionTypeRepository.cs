using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionTypeRepository : GenericRepository<QuestionType>, IQuestionTypeRepository
{
  public QuestionTypeRepository(ApplicationDbContext context) : base(context)
  {
  }
}
