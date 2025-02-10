
namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionOptionRepository : GenericRepository<QuestionOption>
{
  public QuestionOptionRepository(ApplicationDbContext context) : base(context)
  {
  }
}
