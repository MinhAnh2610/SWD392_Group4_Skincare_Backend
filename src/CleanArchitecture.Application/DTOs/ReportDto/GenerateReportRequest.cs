namespace CleanArchitecture.Application.DTOs.ReportDto
{
  public class GenerateReportRequest(
    string? format,
    string? type,
    DateTime? fromDate,
    DateTime? toDate)
  {
    public string? Format { get; set; } = format;
    public string? Type { get; set; } = type;
    public DateTime? FromDate { get; set; } = fromDate;
    public DateTime? ToDate { get; set; } = toDate;
  }
}