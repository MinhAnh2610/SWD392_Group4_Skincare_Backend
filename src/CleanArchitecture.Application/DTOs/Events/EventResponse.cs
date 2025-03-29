namespace CleanArchitecture.Application.DTOs.Events
{
  public class EventResponse
  {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public bool IsActive { get; set; }
  }
}