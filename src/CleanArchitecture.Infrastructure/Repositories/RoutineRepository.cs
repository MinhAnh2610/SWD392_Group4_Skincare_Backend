
namespace CleanArchitecture.Infrastructure.Repositories;

public class RoutineRepository : GenericRepository<Routine>
{
  public RoutineRepository(ApplicationDbContext context) : base(context)
  {
  }
}
