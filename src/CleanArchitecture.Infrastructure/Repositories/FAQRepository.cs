using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class FAQRepository : GenericRepository<FAQ>, IFAQRepository
{
  public FAQRepository(ApplicationDbContext context) : base(context)
  {
  }
}
