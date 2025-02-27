namespace CleanArchitecture.Application.DTOs.ReportDto
{
  public class CosmeticSoldDto
  {
    public Guid CosmeticId { get; set; }
    public string? CosmeticName { get; set; }
    public int NumberOfSales { get; set; }
    public decimal Revenue { get; set; }
  }
}