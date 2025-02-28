namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICartRepository : IGenericRepository<Cart>
{
  Task ClearCartItemsAsync(Guid cartId);
  Task<Cart?> GetCartWithItemsAsync(Guid cartId);
}
