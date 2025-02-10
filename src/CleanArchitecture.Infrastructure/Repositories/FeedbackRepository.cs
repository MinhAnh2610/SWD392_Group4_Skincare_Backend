
namespace CleanArchitecture.Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>
{
  public FeedbackRepository(ApplicationDbContext context) : base(context)
  {
  }
}
