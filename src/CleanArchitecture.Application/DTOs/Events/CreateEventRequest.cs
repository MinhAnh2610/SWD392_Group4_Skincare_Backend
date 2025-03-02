namespace CleanArchitecture.Application.DTOs.Events
{
  public class CreateEventRequest
  {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
  }
}