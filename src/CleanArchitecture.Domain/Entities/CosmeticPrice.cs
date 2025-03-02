namespace CleanArchitecture.Domain.Entities
{
  public class CosmeticPrice : Entity<Guid>
  {
    public Guid CosmeticId { get; set; }
    public Cosmetic? Cosmetic { get; set; }
    public decimal OriginalPrice { get; set; }
    public Guid EventId { get; set; }
    public Event? Event { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}