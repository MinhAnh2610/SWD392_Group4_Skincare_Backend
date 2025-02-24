using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IReportService
  {
    Result<byte[]> GenerateReport(GenerateReportRequest request); 
  }
}