using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuizResultRepository : GenericRepository<QuizResult>, IQuizResultRepository
{
  public QuizResultRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
