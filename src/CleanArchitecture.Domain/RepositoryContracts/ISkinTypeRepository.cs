namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ISkinTypeRepository : IGenericRepository<SkinType>
{
  Task<SkinType?> FindSkinTypeBasedOnBaumannAsync(string name);
}
