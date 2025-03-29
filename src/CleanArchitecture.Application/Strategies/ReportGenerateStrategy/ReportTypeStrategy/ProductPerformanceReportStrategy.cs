using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy
{
  public class ProductPerformanceReportStrategy : IReportTypeStrategy
  {
    public async Task<List<CosmeticSoldDto>> GenerateListAsync(GenerateReportRequest request,
      ReportQueries reportQueries)
    {
      var items = await (
          from order in reportQueries.orderQuery
          join orderItem in reportQueries.orderItemQuery on order.Id equals orderItem.OrderId
          join cosmetic in reportQueries.cosmeticQuery on orderItem.CosmeticId equals cosmetic.Id
          group new { orderItem, cosmetic } by new { orderItem.CosmeticId, cosmetic.Name }
          into g
          select new CosmeticSoldDto
          {
            CosmeticId = g.Key.CosmeticId,
            CosmeticName = g.Key.Name,
            NumberOfSales = g.Sum(x => x.orderItem.Quantity),
            Revenue = g.Sum(x => x.orderItem.SellingPrice)
          }
        )
        .OrderByDescending(cosmetic=> cosmetic.Revenue)
        .ToListAsync();

      return items;
    }
  }
}