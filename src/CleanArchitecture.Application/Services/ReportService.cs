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
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IErrorFactory errorFactory, IEnumerable<IReportGenerateStrategy> reportGenerateStrategies,
      IUnitOfWork unitOfWork)
    {
      _errorFactory = errorFactory;
      _reportGenerateStrategies = reportGenerateStrategies;
      _unitOfWork = unitOfWork;
    }

    public async Task<Result<byte[]>> GenerateReport(GenerateReportRequest request)
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
      
      var cosmeticSales = await (
        from orders in orderQuery
        join orderItem in orderItemQuery on orders.Id equals orderItem.OrderId
        join cosmetics in cosmeticQuery on orderItem.CosmeticId equals cosmetics.Id
        where orders.OrderDate >= request.FromDate && orders.OrderDate <= request.ToDate
        group new { orderItem, cosmetics } by new { cosmetics.Id, cosmetics.Name, cosmetics.Price}
        into g
        select new CosmeticSoldDto()
        {
          CosmeticId = g.Key.Id,
          CosmeticName = g.Key.Name,
          NumberOfSales = g.Sum(x => x.orderItem.Quantity),
          Revenue = g.Sum(x => x.orderItem.Quantity) * g.Key.Price
        }
      ).ToListAsync();
      
      decimal totalRevenue = cosmeticSales.Sum(cs => cs.Revenue);

      ReportContent reportContent = new(cosmeticSales, totalRevenue, request.FromDate, request.ToDate);

      foreach (var strategy in _reportGenerateStrategies)
      {
        var nameOfStrat = strategy.GetType().Name;
        if (nameOfStrat.Contains(request.Format, StringComparison.OrdinalIgnoreCase))
        {
          reportFile = strategy.Generate(request, reportContent);
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