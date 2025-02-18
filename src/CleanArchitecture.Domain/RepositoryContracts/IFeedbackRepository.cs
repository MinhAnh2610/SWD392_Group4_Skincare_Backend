namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
  Task<List<Feedback>> GetAllFeedbacksAsync();
  Task<List<Feedback>> GetFeedbacksByCustomerIdAsync(Guid customerId);
}
