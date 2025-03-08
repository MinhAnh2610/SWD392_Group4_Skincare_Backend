namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ICouponRepository : IGenericRepository<Coupon>
{
  public Task<Coupon?> GetByIdAsync(Guid? id);
}
