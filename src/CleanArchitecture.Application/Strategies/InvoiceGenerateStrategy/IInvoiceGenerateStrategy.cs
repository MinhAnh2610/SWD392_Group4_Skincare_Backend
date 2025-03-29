using CleanArchitecture.Application.DTOs.OrderDto;

namespace CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy
{
  public interface IInvoiceGenerateStrategy
  {
    Task<byte[]> GenerateAsync(Order order, IUnitOfWork unitOfWork);
  }
}