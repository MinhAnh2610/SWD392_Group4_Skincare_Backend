namespace CleanArchitecture.Domain.RepositoryContracts
{
  public interface ICosmeticPriceRepository : IGenericRepository<CosmeticPrice>
  {
    Task<CosmeticPrice?> GetByCosmeticIdAsync(Guid cosmeticId);
  }
}