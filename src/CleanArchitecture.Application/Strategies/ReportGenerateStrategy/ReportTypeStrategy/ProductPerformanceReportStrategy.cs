using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy
{
  public class ProductPerformanceReportStrategy : IReportTypeStrategy
  {
    public async Task<List<CosmeticSoldDto>> GenerateListAsync(GenerateReportRequest request, IQueryable<Order> orderQuery, IQueryable<OrderItem> orderItemQuery,
      IQueryable<Cosmetic> cosmeticQuery)
    {
      var cosmeticSales = await (
          from order in orderQuery
          join orderItem in orderItemQuery on order.Id equals orderItem.OrderId
          join cosmetic in cosmeticQuery on orderItem.CosmeticId equals cosmetic.Id
          group new { orderItem, cosmetic } by new { cosmetic.Id, cosmetic.Name, cosmetic.Price } into g
          select new CosmeticSoldDto
          {
            CosmeticId = g.Key.Id,
            CosmeticName = g.Key.Name,
            NumberOfSales = g.Sum(x => x.orderItem.Quantity),
            Revenue = g.Sum(x => x.orderItem.Quantity) * g.Key.Price
          }
        )
        .OrderByDescending(dto => dto.Revenue)
        .ToListAsync();

      return cosmeticSales;
    }
  }
}