using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
  public QuizRepository(ApplicationDbContext context) : base(context)
  {
  }
}
