using CleanArchitecture.Application.DTOs.ReportDto;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Strategies;
using CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class ReportService : IReportService
  {
    private readonly IErrorFactory _errorFactory;
    private readonly IEnumerable<IReportGenerateStrategy> _reportGenerateStrategies;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IReportTypeStrategy> _reportTypeStrategies;

    public ReportService(IErrorFactory errorFactory, IEnumerable<IReportGenerateStrategy> reportGenerateStrategies,
      IUnitOfWork unitOfWork, IEnumerable<IReportTypeStrategy> reportTypeStrategies)
    {
      _errorFactory = errorFactory;
      _reportGenerateStrategies = reportGenerateStrategies;
      _unitOfWork = unitOfWork;
      _reportTypeStrategies = reportTypeStrategies;
    }

    public async Task<Result<byte[]>> GenerateReportAsync(GenerateReportRequest request)
    {
      byte[] reportFile = [];

      var orderQuery = _unitOfWork.Orders.GetQueryable();
      var orderItemQuery = _unitOfWork.OrderItems.GetQueryable();
      var cosmeticQuery = _unitOfWork.Cosmetics.GetQueryable();

      var isValidDates = IsValidTimePeriod(request.FromDate, request.ToDate);
      if (!isValidDates)
      {
        var error = _errorFactory.CreateInvalidDates();
        return Result<byte[]>.Failure([error.err], error.statusCode);
      }

      var reportType = request.Type;
      decimal totalRevenue = 0;
      List<CosmeticSoldDto> cosmeticSales = new List<CosmeticSoldDto>();
      foreach (var strategy in _reportTypeStrategies)
      {
        var nameOfStrat = strategy.GetType().Name;
        if (nameOfStrat.Contains(reportType, StringComparison.OrdinalIgnoreCase))
        {
          cosmeticSales = await strategy.GenerateListAsync(request, orderQuery, orderItemQuery, cosmeticQuery);
          totalRevenue = cosmeticSales.Sum(x => x.Revenue);
          break;
        }
      }
      
      ReportContent reportContent = new(cosmeticSales, totalRevenue, request.FromDate, request.ToDate);
      
      foreach (var strategy in _reportGenerateStrategies)
      {
        var nameOfStrat = strategy.GetType().Name;
        if (nameOfStrat.Contains(request.Format, StringComparison.OrdinalIgnoreCase))
        {
          reportFile = strategy.Generate(request, reportContent);
          break;
        }
      }

      
      if (reportFile.Length <= 0)
      {
        var error = _errorFactory.CreateFileCreatedFailed($"Report{request.Format}");
        return Result<byte[]>.Failure([error.err], error.statusCode);
      }

      return Result<byte[]>.Success(reportFile, StatusCodes.Status200OK);
    }

    private bool IsValidTimePeriod(DateTime? fromDate, DateTime? toDate)
    {
      if (!fromDate.HasValue || !toDate.HasValue)
      {
        return false;
      }

      if (fromDate > toDate)
      {
        return false;
      }

      if (fromDate > DateTime.Now || toDate > DateTime.Now)
      {
        return false;
      }

      return true;
    }
  }
}