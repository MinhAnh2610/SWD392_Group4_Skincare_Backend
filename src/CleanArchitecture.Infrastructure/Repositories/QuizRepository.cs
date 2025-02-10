
namespace CleanArchitecture.Infrastructure.Repositories;

public class QuizRepository : GenericRepository<Quiz>
{
  public QuizRepository(ApplicationDbContext context) : base(context)
  {
  }
}
