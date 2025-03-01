namespace CleanArchitecture.Application.DTOs.GHN;

public class GHNOrderItemRequest
{
  public required string Name { get; set; }
  public string? Code { get; set; }
  public required int Quantity { get; set; }
  public decimal Price { get; set; }
  public int Length { get; set; }
  public int Width { get; set; }
  public int Height { get; set; }
  public required int Weight { get; set; }
}
