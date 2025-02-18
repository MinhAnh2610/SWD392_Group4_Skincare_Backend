namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IBrandRepository : IGenericRepository<Brand>
{
  Task<List<Brand>> GetAllBrandsAsync();
}
