namespace CleanArchitecture.Domain.RepositoryContracts
{
  public interface IUserCouponRepository : IGenericRepository<UserCoupon>
  {
    Task<UserCoupon?> GetByIdAsync(Guid userId, Guid couponId);
    Task<List<UserCoupon>> GetUserCouponsAsync(User user);
  }
}