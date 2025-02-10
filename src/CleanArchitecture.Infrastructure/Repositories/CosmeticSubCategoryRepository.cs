namespace CleanArchitecture.Infrastructure.Repositories;

public class CosmeticSubCategoryRepository : GenericRepository<CosmeticSubCategory>
{
  public CosmeticSubCategoryRepository(ApplicationDbContext context) : base(context)
  {
  }
}
