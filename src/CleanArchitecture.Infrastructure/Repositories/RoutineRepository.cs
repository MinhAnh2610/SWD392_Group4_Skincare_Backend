using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class RoutineRepository : GenericRepository<Routine>, IRoutineRepository
{
  public RoutineRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<List<Routine>> GetAllAsync()
  {
    return await _context.Routines
      .Include(r => r.SkinType)
      .Include(r => r.RoutineSteps)
      .ToListAsync();
  }
}
