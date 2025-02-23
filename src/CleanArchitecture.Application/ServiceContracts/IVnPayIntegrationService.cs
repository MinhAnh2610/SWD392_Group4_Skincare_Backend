using CleanArchitecture.Application.DTOs.VnPay;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IVnPayIntegrationService
  {
    string CreatePaymentUrl(VnPayPaymentRequestDto request, HttpContext context);
    Task<VnPayPaymentResponseDto> ProcessReturnAsync(IQueryCollection query);
  }
}
