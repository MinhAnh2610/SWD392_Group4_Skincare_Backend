namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IRoutineRepository : IGenericRepository<Routine>
{
  Task<Routine?> GetRoutineBySkinTypeAsync(Guid SkinTypeId);
}
