using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy
{
  public class RevenueReportStrategy : IReportTypeStrategy
  {
    public async Task<List<CosmeticSoldDto>> GenerateListAsync(GenerateReportRequest request, IQueryable<Order> orderQuery, IQueryable<OrderItem> orderItemQuery, IQueryable<Cosmetic> cosmeticQuery)
    {
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
      return cosmeticSales;
    }
  }
}