using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies
{
  public class WordReportGenerateStrategy : IReportGenerateStrategy
  {
    public byte[] Generate(GenerateReportRequest request, ReportContent reportContent)
    {
      return [];
    }
  }
}