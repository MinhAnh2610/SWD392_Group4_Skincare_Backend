namespace CleanArchitecture.Domain.Entities;

public class Coupon : Entity<Guid>
{
  public string? Name { get; set; }
  public string? Code { get; set; }
  public double DiscountAmount { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public int UsageLimit { get; set; }
  public List<Order> Orders { get; set; } = new List<Order>();
}