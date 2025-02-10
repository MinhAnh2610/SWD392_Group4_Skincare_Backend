using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class PolicyRepository : GenericRepository<Policy>, IPolicyRepository
{
  public PolicyRepository(ApplicationDbContext context) : base(context)
  {
  }
}
