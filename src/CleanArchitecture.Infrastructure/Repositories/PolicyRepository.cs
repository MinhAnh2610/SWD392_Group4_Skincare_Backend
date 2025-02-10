
namespace CleanArchitecture.Infrastructure.Repositories;

public class PolicyRepository : GenericRepository<Policy>
{
  public PolicyRepository(ApplicationDbContext context) : base(context)
  {
  }
}
