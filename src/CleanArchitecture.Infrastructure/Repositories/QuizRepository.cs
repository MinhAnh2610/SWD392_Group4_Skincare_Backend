using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
  public QuizRepository(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<List<Quiz>> GetAllAsync()
  {
    return await _context.Quizzes
      .Include(q => q.Questions)
        .ThenInclude(q => q.QuestionType)
      .Include(q => q.Questions)
        .ThenInclude(q => q.QuestionOptions)
      .ToListAsync();
  }

  public override async Task<Quiz?> GetByIdAsync(Guid id)
  {
    return await _context.Quizzes
      .Include(q => q.Questions)
        .ThenInclude(q => q.QuestionType)
      .Include(q => q.Questions)
        .ThenInclude(q => q.QuestionOptions)
      .FirstOrDefaultAsync(q => q.Id == id);
  }
}
