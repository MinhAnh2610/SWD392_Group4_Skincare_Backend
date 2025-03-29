namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IUserRepository : IGenericRepository<User>
{
  Task<User?> GetByPhoneNumberAsync(string phoneNumber);
}
