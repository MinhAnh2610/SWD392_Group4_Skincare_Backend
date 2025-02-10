
namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionTypeRepository : GenericRepository<QuestionType>
{
  public QuestionTypeRepository(ApplicationDbContext context) : base(context)
  {
  }
}
