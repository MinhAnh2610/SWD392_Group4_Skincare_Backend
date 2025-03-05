using Abp.Extensions;
using CleanArchitecture.Application.DTOs.ReportDto;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Strategies;
using CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace CleanArchitecture.Application.Services
{
  public class ReportService : IReportService
  {
    private readonly IErrorFactory _errorFactory;
    private readonly IEnumerable<IReportGenerateStrategy> _reportGenerateStrategies;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IReportTypeStrategy> _reportTypeStrategies;
    private readonly ITimeZoneService _timeZoneService;

    public ReportService(IErrorFactory errorFactory, IEnumerable<IReportGenerateStrategy> reportGenerateStrategies,
      IUnitOfWork unitOfWork, IEnumerable<IReportTypeStrategy> reportTypeStrategies, ITimeZoneService timeZoneService)
    {
      _errorFactory = errorFactory;
      _reportGenerateStrategies = reportGenerateStrategies;
      _unitOfWork = unitOfWork;
      _reportTypeStrategies = reportTypeStrategies;
      _timeZoneService = timeZoneService;
    }

    public async Task<Result<byte[]>> GenerateReportAsync(GenerateReportRequest request)
    {
      var validationResult = IsValidReportRequest(request);
      if (!validationResult.isValid)
      {
        var error = new Error("GenerateReportRequest.Invalid", validationResult.errorMessage);  
        return Result<byte[]>.Failure([error], StatusCodes.Status400BadRequest);
      }
      
      byte[] reportFile = [];
      //TODO: HERE
      var orderQuery = _unitOfWork.Orders.GetQueryable();
      var orderItemQuery = _unitOfWork.OrderItems.GetQueryable();
      var cosmeticQuery = _unitOfWork.Cosmetics.GetQueryable();
      var cosmeticPriceQuery = _unitOfWork.CosmeticPrices.GetQueryable();
      ReportQueries reportQueries = new ReportQueries(orderQuery, orderItemQuery, cosmeticQuery, cosmeticPriceQuery);
      
      var reportType = request.Type;
      decimal totalRevenue = 0;
      List<CosmeticSoldDto> cosmeticSales = new List<CosmeticSoldDto>();
      foreach (var strategy in _reportTypeStrategies)
      {
        var nameOfStrat = strategy.GetType().Name;
        if (nameOfStrat.Contains(reportType, StringComparison.OrdinalIgnoreCase))
        {
          cosmeticSales = await strategy.GenerateListAsync(request, reportQueries);
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

    private (bool isValid , string errorMessage) IsValidReportRequest(GenerateReportRequest request)
    {
      StringBuilder errorMessage = new StringBuilder();
      bool isValidDate = true;
      bool isValidType = true;
      bool isValidFormat = true;
      if (!request.FromDate.HasValue || !request.ToDate.HasValue)
      {
        isValidDate = false;
        errorMessage.AppendLine("ToDate and FromDate is required");
      }

      if (request.FromDate > request.ToDate)
      {
        isValidDate = false;
        errorMessage.AppendLine("FromDate cannot be greater than ToDate");
      }

      var dateTimeNow = _timeZoneService.ConvertToLocalTime(DateTime.UtcNow);

      if (request.FromDate > dateTimeNow || request.ToDate > dateTimeNow)
      {
        isValidDate = false;
        errorMessage.AppendLine("FromDate and ToDate cannot be greater than now");
      }

      if (!ReportType.ReportTypes.Contains(request.Type, StringComparer.OrdinalIgnoreCase))
      {
        isValidType = false;
        errorMessage.AppendLine("Report type is not valid");
      }

      if (!ReportFormat.Formats.Contains(request.Format, StringComparer.OrdinalIgnoreCase))
      {
        isValidFormat = false;
        errorMessage.AppendLine("Format is not valid");
      }

      return (isValidDate && isValidType && isValidFormat, errorMessage.ToString());
    }
  }
}