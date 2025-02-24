using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies
{
  public interface IReportGenerateStrategy
  {
    byte[] Generate(GenerateReportRequest request);
  }
}