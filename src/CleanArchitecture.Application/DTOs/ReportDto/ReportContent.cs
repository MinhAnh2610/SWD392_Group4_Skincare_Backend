namespace CleanArchitecture.Application.DTOs.ReportDto
{
  public class ReportContent
  {

    public ReportContent(List<CosmeticSoldDto> cosmeticsSold, decimal totalRevenue, DateTime? startDate, DateTime? toDate)
    {
      CosmeticsSold = cosmeticsSold;
      TotalRevenue = totalRevenue;
      StartDate = startDate;
      ToDate = toDate;
    }
    public List<CosmeticSoldDto> CosmeticsSold { get; set; }
    public decimal TotalRevenue { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ToDate { get; set; }
  }
}