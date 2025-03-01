using CleanArchitecture.Application.DTOs.OrderDto;

namespace CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy
{
  public interface IInvoiceGenerateStrategy
  {
    byte[] Generate(Order order);
  }
}