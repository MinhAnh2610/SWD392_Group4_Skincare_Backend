namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICosmeticImageRepository : IGenericRepository<CosmeticImage>
{
  Task<List<CosmeticImage>> GetAllCosmeticImagesAsync();
}
