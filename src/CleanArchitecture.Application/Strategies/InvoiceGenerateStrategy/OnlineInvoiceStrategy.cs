using CleanArchitecture.Application.DTOs.OrderDto;

namespace CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy
{
  public class WalkInInvoiceStrategy: IInvoiceGenerateStrategy
  {
    public byte[] Generate(Order order)
    {
      throw new NotImplementedException();
    }
  }
}