using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories
{
  public class PlayLogRepository : GenericRepository<PlayLog>, IPlayLogRepository
  {
    public PlayLogRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<int> GetPlayTimesAsync(string time, User user)
    {
      // Get the current date without time for consistent calculations
      var today = DateTime.Today;
      DateTime start;
      DateTime end;

      // Determine the start and end dates based on the time parameter
      switch (time.ToLower())
      {
        case "day":
          start = today;                // Start of today (00:00:00)
          end = today.AddDays(1);       // Start of tomorrow (00:00:00)
          break;
        case "month":
          start = new DateTime(today.Year, today.Month, 1); // First day of current month
          end = start.AddMonths(1);                         // First day of next month
          break;
        case "year":
          start = new DateTime(today.Year, 1, 1);  // First day of current year
          end = start.AddYears(1);                  // First day of next year
          break;
        default:
          throw new ArgumentException("Invalid time frame. Use 'day', 'month', or 'year'.", nameof(time));
      }

      // Query PlayLogs and count entries for the user within the time frame
      return await _context.PlayLogs
        .Where(pl => pl.UserId == user.Id && pl.PlayTimeStamp>= start && pl.PlayTimeStamp < end)
        .CountAsync();
    }
  }
}