using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IReportService
  {
    Task<Result<byte[]>> GenerateReportAsync(GenerateReportRequest request);
  }
}