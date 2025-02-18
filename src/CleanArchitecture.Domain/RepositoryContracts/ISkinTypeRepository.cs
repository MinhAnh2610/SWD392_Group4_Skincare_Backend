namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ISkinTypeRepository : IGenericRepository<SkinType>
{
  Task<List<SkinType>> GetAllSkinTypesAsync();
}
