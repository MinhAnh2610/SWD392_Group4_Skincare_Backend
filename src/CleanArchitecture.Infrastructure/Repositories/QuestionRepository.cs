
namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionRepository : GenericRepository<Question>
{
  public QuestionRepository(ApplicationDbContext context) : base(context)
  {
  }
}
