namespace CleanArchitecture.Domain.RepositoryContracts
{
  public interface IPlayLogRepository : IGenericRepository<PlayLog>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time">Time type like month, date, year</param>
    /// <returns></returns>
    Task<int> GetPlayTimesAsync(string time, User user);
  }
}