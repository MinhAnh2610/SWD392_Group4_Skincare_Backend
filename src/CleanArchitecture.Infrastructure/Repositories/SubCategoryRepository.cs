namespace CleanArchitecture.Infrastructure.Repositories;

public class SubCategoryRepository : GenericRepository<SubCategory>
{
  public SubCategoryRepository(ApplicationDbContext context) : base(context)
  {
  }
}
