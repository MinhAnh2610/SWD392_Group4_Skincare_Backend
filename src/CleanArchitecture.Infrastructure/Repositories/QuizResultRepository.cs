using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class QuizResultRepository : GenericRepository<QuizResult>, IQuizResultRepository
{
  public QuizResultRepository(ApplicationDbContext context) : base(context)
  {
    
  }

  public override async Task<List<QuizResult>> GetAllAsync()
  {
    return await _context.QuizResults
      .Include(qr => qr.Customer)
      .Include(qr => qr.Quiz)
      .Include(qr => qr.SkinType)
      .Include(qr => qr.QuizAnswers)
        .ThenInclude(qa => qa.Question).ToListAsync();
  }
}
