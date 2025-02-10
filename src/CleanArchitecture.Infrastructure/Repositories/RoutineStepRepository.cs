using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class RoutineStepRepository : GenericRepository<RoutineStep>, IRoutineStepRepository
{
  public RoutineStepRepository(ApplicationDbContext context) : base(context)
  {
  }
}
