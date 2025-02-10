namespace CleanArchitecture.Infrastructure.Repositories;

public class BatchRepository : GenericRepository<Batch>
{
  public BatchRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
