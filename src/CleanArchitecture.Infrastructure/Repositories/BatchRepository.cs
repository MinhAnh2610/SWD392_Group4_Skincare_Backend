using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class BatchRepository : GenericRepository<Batch>, IBatchRepository
{
  public BatchRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
