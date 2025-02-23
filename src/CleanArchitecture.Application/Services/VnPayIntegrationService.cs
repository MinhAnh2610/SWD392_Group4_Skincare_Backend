using Application.Library;
using CleanArchitecture.Application.DTOs.VnPay;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

    public Result<string> CreatePaymentUrl(VnPayPaymentRequestDto request, HttpContext context)
    {
      try
      {
        // Convert UTC to configured Vietnamese timezone.
        var timeZoneId = _configuration["TimeZoneId"] ?? "SE Asia Standard Time";
        var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);

        // Use OrderId (as a GUID string) as transaction reference.
        string txnRef = request.OrderId.ToString();

        // Set up the required VNPay parameters.
        _vnPayLibrary.AddRequestData("vnp_Version", "2.1.0");
        _vnPayLibrary.AddRequestData("vnp_Command", "pay");
        _vnPayLibrary.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
        // Multiply by 100 (assuming request.Amount is in VND)
        _vnPayLibrary.AddRequestData("vnp_Amount", ((int)request.Amount * 100).ToString());
        _vnPayLibrary.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
        _vnPayLibrary.AddRequestData("vnp_CurrCode", "VND");
        _vnPayLibrary.AddRequestData("vnp_IpAddr", _vnPayLibrary.GetIpAddress(context));
        _vnPayLibrary.AddRequestData("vnp_Locale", "vn");
        // Use OrderId in the OrderInfo for display.
        _vnPayLibrary.AddRequestData("vnp_OrderInfo", $"Payment for order {request.OrderId}");
        _vnPayLibrary.AddRequestData("vnp_OrderType", "other");
        _vnPayLibrary.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:ReturnUrl"]);
        _vnPayLibrary.AddRequestData("vnp_TxnRef", txnRef);

        var paymentUrl = _vnPayLibrary.CreateRequestUrl(
            _configuration["Vnpay:BaseUrl"],
            _configuration["Vnpay:HashSecret"]);

        return Result<string>.Success(paymentUrl, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        var errors = new List<Error> { new Error("Payment.CreatePayment", ex.Message) };
        return Result<string>.Failure(errors, StatusCodes.Status400BadRequest);
      }
    }

    public async Task<Result<VnPayPaymentResponseDto>> ProcessReturnAsync(IQueryCollection query)
    {
      try
      {
        // Process the VNPay callback and validate the secure hash.
        var response = _vnPayLibrary.GetFullResponseData(query, _configuration["Vnpay:HashSecret"]);

        if (!response.Success)
        {
          return Result<VnPayPaymentResponseDto>.Failure(
              new List<Error> { VnPayErrors.SignatureValidationFailed },
              StatusCodes.Status400BadRequest);
        }

        // Expect vnp_TxnRef to be a GUID (representing the OrderId).
        if (!Guid.TryParse(response.TransactionOrderId, out var orderId))
        {
          response.Success = false;
          response.OrderDescription = "Invalid Order Id in response";
          return Result<VnPayPaymentResponseDto>.Failure(
              new List<Error> { VnPayErrors.InvalidOrderId },
              StatusCodes.Status400BadRequest);
        }

        // Read the total amount from vnp_Amount and convert back to VND.
        if (!decimal.TryParse(response.TotalAmount, out var amountInt))
        {
          amountInt = 0;
        }
        decimal totalAmount = amountInt / 100m;

        // Create and save a Payment entity.
        var payment = new Payment
        {
          OrderId = orderId,
          TransactionId = response.TransactionId, // VNPAY's transaction number
          Method = "VNPay",
          TotalAmount = totalAmount,
          Date = DateTime.Now // Or parse vnp_CreateDate if needed.
        };

        await _unitOfWork.Payments.CreateAsync(payment);

        return Result<VnPayPaymentResponseDto>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        var errors = new List<Error> { new Error("Payment.VnPayReturn", ex.Message) };
        return Result<VnPayPaymentResponseDto>.Failure(errors, StatusCodes.Status400BadRequest);
      }
    }
  }
}
