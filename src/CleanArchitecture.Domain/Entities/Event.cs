namespace CleanArchitecture.Domain.Entities
{
  public class Event : Entity<Guid>
  {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public List<CosmeticPrice>? CosmeticPrices { get; set; } = new List<CosmeticPrice>();
  }
}