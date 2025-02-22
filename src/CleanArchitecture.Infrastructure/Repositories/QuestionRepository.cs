using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
  public QuestionRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<List<Question>> GetAllAsync()
  {
    return await _context.Questions
      .Include(q => q.QuestionOptions)
      .ToListAsync();
  }
}
