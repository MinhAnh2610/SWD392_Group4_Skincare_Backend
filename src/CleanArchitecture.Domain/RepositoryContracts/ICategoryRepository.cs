namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICategoryRepository : IGenericRepository<Category>
{
  Task<List<Category>> GetCategoriesAsync();  
}
