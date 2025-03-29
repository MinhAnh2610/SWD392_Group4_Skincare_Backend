using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
  public TagRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
