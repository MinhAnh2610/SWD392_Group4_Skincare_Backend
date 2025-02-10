
namespace CleanArchitecture.Infrastructure.Repositories;

public class RoutineStepRepository : GenericRepository<RoutineStep>
{
  public RoutineStepRepository(ApplicationDbContext context) : base(context)
  {
  }
}
