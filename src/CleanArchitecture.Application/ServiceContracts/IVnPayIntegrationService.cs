using CleanArchitecture.Application.DTOs.VnPay;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IVnPayIntegrationService
  {
    Result<string> CreatePaymentUrl(VnPayPaymentRequestDto request, HttpContext context);
    Task<Result<VnPayPaymentResponseDto>> ProcessReturnAsync(IQueryCollection query);
  }
}
