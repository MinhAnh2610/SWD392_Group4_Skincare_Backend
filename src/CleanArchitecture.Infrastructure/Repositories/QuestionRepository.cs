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

  public override async Task<Question?> GetByIdAsync(Guid code)
  {
    return await _context.Questions
      .Include(q => q.QuestionOptions)
      .FirstOrDefaultAsync(q => q.Id == code);
  }
}
