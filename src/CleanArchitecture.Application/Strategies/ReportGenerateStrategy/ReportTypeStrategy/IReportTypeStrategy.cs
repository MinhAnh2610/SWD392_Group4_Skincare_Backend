using CleanArchitecture.Application.DTOs.ReportDto;

namespace CleanArchitecture.Application.Strategies.ReportGenerateStrategy.ReportTypeStrategy
{
  public interface IReportTypeStrategy
  {
    Task<List<CosmeticSoldDto>> GenerateListAsync(GenerateReportRequest request, IQueryable<Order> orderQuery, IQueryable<OrderItem> orderItemQuery, IQueryable<Cosmetic> cosmeticQuery);
  }
}