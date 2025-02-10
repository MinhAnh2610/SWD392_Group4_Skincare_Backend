using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionOptionRepository : GenericRepository<QuestionOption>, IQuestionOptionRepository
{
  public QuestionOptionRepository(ApplicationDbContext context) : base(context)
  {
  }
}
