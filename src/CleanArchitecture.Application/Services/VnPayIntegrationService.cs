using Application.Library;
using CleanArchitecture.Application.DTOs.VnPay;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
  public class VnPayIntegrationService : IVnPayIntegrationService
  {
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly VnPayLibrary _vnPayLibrary;

    public VnPayIntegrationService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
      _configuration = configuration;
      _unitOfWork = unitOfWork;
      _vnPayLibrary = new VnPayLibrary();
    }

    public string CreatePaymentUrl(VnPayPaymentRequestDto request, HttpContext context)
    {
      // Use a proper Vietnamese timezone from config (e.g. "SE Asia Standard Time")
      var timeZoneId = _configuration["TimeZoneId"] ?? "SE Asia Standard Time";
      var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
      var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);

      // Use the OrderId as the transaction reference.
      string txnRef = request.OrderId.ToString();

      // Set up request data.
      _vnPayLibrary.AddRequestData("vnp_Version", "2.1.0");
      _vnPayLibrary.AddRequestData("vnp_Command", "pay");
      _vnPayLibrary.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
      // Multiply by 100 (assuming Amount is in VND)
      _vnPayLibrary.AddRequestData("vnp_Amount", ((int)request.Amount * 100).ToString());
      _vnPayLibrary.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
      _vnPayLibrary.AddRequestData("vnp_CurrCode", "VND");
      _vnPayLibrary.AddRequestData("vnp_IpAddr", _vnPayLibrary.GetIpAddress(context));
      _vnPayLibrary.AddRequestData("vnp_Locale", "vn");
      // Include the OrderId in the OrderInfo for display purposes.
      _vnPayLibrary.AddRequestData("vnp_OrderInfo", $"Payment for order {request.OrderId}");
      _vnPayLibrary.AddRequestData("vnp_OrderType", "other");
      _vnPayLibrary.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:ReturnUrl"]);
      _vnPayLibrary.AddRequestData("vnp_TxnRef", txnRef);

      var paymentUrl = _vnPayLibrary.CreateRequestUrl(
          _configuration["Vnpay:BaseUrl"],
          _configuration["Vnpay:HashSecret"]);

      return paymentUrl;
    }

    public async Task<VnPayPaymentResponseDto> ProcessReturnAsync(IQueryCollection query)
    {
      // Get the full response from VNPAY
      var response = _vnPayLibrary.GetFullResponseData(query, _configuration["Vnpay:HashSecret"]);

      if (!response.Success)
      {
        return response;
      }

      // For our integration, we expect vnp_TxnRef to be the OrderId.
      if (!Guid.TryParse(response.TransactionOrderId, out var orderId))
      {
        // If the OrderId is not a valid Guid, return a failure.
        response.Success = false;
        response.OrderDescription = "Invalid Order Id in response";
        return response;
      }

      // Use the total amount from VNPAY's response (vnp_Amount) and convert it back to VND.
      if (!decimal.TryParse(response.TotalAmount, out var amountInt))
      {
        amountInt = 0;
      }
      decimal totalAmount = amountInt / 100m;

      // Create a new Payment entity.
      var payment = new Payment
      {
        OrderId = orderId,
        TransactionId = response.TransactionId, // VNPAY's transaction number
        Method = "VNPay",
        TotalAmount = totalAmount,
        Date = DateTime.Now // Or parse from vnp_CreateDate if you prefer
      };

      // Save the payment record.
      await _unitOfWork.Payments.CreateAsync(payment);

      return response;
    }

  }
}
