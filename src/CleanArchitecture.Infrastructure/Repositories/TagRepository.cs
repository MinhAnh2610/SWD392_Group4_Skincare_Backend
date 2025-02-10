namespace CleanArchitecture.Infrastructure.Repositories;

public class TagRepository : GenericRepository<Tag>
{
  public TagRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
