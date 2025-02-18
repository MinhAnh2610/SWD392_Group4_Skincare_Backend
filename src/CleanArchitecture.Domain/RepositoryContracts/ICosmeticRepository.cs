namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICosmeticRepository : IGenericRepository<Cosmetic>
{
  public Task<List<Cosmetic>> GetCosmeticsAsync();
}
