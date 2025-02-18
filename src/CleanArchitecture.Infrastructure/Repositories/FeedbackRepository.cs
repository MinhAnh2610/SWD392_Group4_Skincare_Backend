using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
  public FeedbackRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<List<Feedback>> GetAllFeedbacksAsync()
  {
    return await _context.Feedbacks
      .Include(f => f.Cosmetic)
      .Include(f => f.Customer)
      .ToListAsync();
  }
}
