using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy
{
  public class RevenueReportStrategy : IReportTypeStrategy
  {
    public async Task<List<CosmeticSoldDto>> GenerateListAsync(GenerateReportRequest request,
      ReportQueries reportQueries)
    {
      
      var items = await
        (from order in reportQueries.orderQuery
          join orderItem in reportQueries.orderItemQuery on order.Id equals orderItem.OrderId
          join cosmetic in reportQueries.cosmeticQuery on orderItem.CosmeticId equals cosmetic.Id
          where order.OrderDate >= request.FromDate && order.OrderDate <= request.ToDate
          group new { orderItem, cosmetic } by new { orderItem.CosmeticId, cosmetic.Name }
          into g
          select new CosmeticSoldDto
          {
            CosmeticId = g.Key.CosmeticId,
            CosmeticName = g.Key.Name,
            NumberOfSales = g.Sum(x => x.orderItem.Quantity),
            Revenue = g.Sum(x => x.orderItem.SellingPrice)
          }).ToListAsync();
      return items;
    }
  }
}