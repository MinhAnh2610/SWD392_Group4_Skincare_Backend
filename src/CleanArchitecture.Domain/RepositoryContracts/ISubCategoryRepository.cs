namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ISubCategoryRepository : IGenericRepository<SubCategory>
{
  Task<List<SubCategory>> GetAllSubCategoriesAsync();
}
