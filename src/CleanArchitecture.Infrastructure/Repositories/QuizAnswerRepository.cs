using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuizAnswerRepository : GenericRepository<QuizAnswer>, IQuizAnswerRepository
{
  public QuizAnswerRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
