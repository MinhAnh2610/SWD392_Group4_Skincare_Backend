namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IRoutineRepository : IGenericRepository<Routine>
{
  Task<List<Routine>?> GetRoutineBySkinTypeAsync(Guid SkinTypeId);
}
