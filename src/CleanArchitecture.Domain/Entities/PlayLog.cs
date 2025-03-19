using System.Net.NetworkInformation;

namespace CleanArchitecture.Domain.Entities
{
  public class PlayLog : Entity<Guid>
  {
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime PlayTimeStamp { get; set; }
  }
}