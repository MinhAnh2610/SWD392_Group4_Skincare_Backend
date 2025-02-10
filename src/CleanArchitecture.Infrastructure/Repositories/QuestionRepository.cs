using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
  public QuestionRepository(ApplicationDbContext context) : base(context)
  {
  }
}
