using CleanArchitecture.Application.DTOs.ReportDto;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Strategies;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class ReportService : IReportService
  {
    private readonly IErrorFactory _errorFactory;
    private readonly IEnumerable<IReportGenerateStrategy> _reportGenerateStrategies;

    public ReportService(IErrorFactory errorFactory, IEnumerable<IReportGenerateStrategy> reportGenerateStrategies)
    {
      _errorFactory = errorFactory;
      _reportGenerateStrategies = reportGenerateStrategies;
    }

    public Result<byte[]> GenerateReport(GenerateReportRequest request)
    {
      byte[] reportFile = [];
      foreach (var strategy in _reportGenerateStrategies)
      {
        var nameOfStrat = strategy.GetType().Name; 
        if (nameOfStrat.Contains(request.Format, StringComparison.OrdinalIgnoreCase))
        {
          reportFile = strategy.Generate(request);
        }
      }

      if (reportFile.Length <= 0)
      {
        var error = _errorFactory.CreateFileCreatedFailed($"Report{request.Format}");
        return Result<byte[]>.Failure([error.err], error.statusCode);
      }
      
      return Result<byte[]>.Success(reportFile, StatusCodes.Status200OK);
    }
  }
}