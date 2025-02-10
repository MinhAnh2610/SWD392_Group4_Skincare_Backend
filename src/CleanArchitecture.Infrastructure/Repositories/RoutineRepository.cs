using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class RoutineRepository : GenericRepository<Routine>, IRoutineRepository
{
  public RoutineRepository(ApplicationDbContext context) : base(context)
  {
  }
}
