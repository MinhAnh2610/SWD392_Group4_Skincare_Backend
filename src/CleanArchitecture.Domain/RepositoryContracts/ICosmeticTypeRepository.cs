namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICosmeticTypeRepository : IGenericRepository<CosmeticType>
{
  Task<List<CosmeticType>> GetAllCosmeticTypesAsync();
}
