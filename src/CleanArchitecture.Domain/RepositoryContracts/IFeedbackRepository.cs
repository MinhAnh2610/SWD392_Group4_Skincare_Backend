namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
  Task<List<Feedback>> GetAllFeedbacksAsync();
}
