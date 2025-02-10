
namespace CleanArchitecture.Infrastructure.Repositories;

public class FAQRepository : GenericRepository<FAQ>
{
  public FAQRepository(ApplicationDbContext context) : base(context)
  {
  }
}
