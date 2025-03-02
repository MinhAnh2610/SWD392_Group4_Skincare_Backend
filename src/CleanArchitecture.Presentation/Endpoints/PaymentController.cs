using Carter;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.DTOs.Payment;
using CleanArchitecture.Application.DTOs.VnPay;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class PaymentController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/payment").WithTags("Payment Management");

      #region Get All Payments API
      group.MapGet("/", async (IPaymentService paymentService) =>
      {
        var result = await paymentService.GetAllPaymentsAsync();
        return result.Match("Retrieved Payments Successfully.");
      })
      .WithName("GetAllPayments")
      .Produces<ApiResponse<List<PaymentResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("GetAllPayments")
      .WithDescription("Get All Payments")
      .RequireAuthorization();
      #endregion

      #region Create Payment URL Endpoint
      group.MapPost("/payment-url", (IVnPayIntegrationService vnPayIntegrationService, HttpContext context, VnPayPaymentRequestDto request) =>
      {
        var result = vnPayIntegrationService.CreatePaymentUrl(request, context);
        return result.Match("Payment URL created successfully.");
      })
      .WithName("CreatePaymentUrl")
      .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Payment URL")
      .WithDescription("Creates a payment URL for VNPay integration.");
      #endregion

      #region Process VNPay Return Endpoint
      group.MapGet("/vnpay-return", async (IVnPayIntegrationService vnPayIntegrationService, IOrderService orderService, HttpContext context) =>
      {
        // Process the VNPay callback.
        var vnPayResult = await vnPayIntegrationService.ProcessReturnAsync(context.Request.Query);
        if (vnPayResult.IsFailure)
        {
          return vnPayResult.Match("Payment processing failed.");
        }

        var vnPayResponse = vnPayResult.Data;
        // Validate that the TransactionOrderId is a valid GUID (the order's ID).
        if (!Guid.TryParse(vnPayResponse.TransactionOrderId, out var orderId))
        {
          return Results.BadRequest(ApiResponse<VnPayPaymentResponseDto>.FailureResponse(
            new List<Error> { new Error("InvalidOrderId", "Invalid Order Id returned from VNPay") },
            "Invalid Order Id returned from VNPay."));
        }

        // Map the VNPay response directly to the PaymentReturnData.
        // (If you prefer, you can use vnPayResponse directly if it has all the needed data.)
        var paymentReturnData = new PaymentReturnData
        {
          TransactionId = vnPayResponse.TransactionId,
          TotalAmount = vnPayResponse.TotalAmount,
          ResponseCode = vnPayResponse.ResponseCode
        };

        // Complete the order using the VNPay response.
        var orderResult = await orderService.CompleteOrder(orderId, vnPayResponse.ResponseCode ?? string.Empty, paymentReturnData);
        return orderResult.Match("Order completed successfully.");
      })
      .WithName("ProcessVnPayReturn")
      .Produces<ApiResponse<VnPayPaymentResponseDto>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Process VNPay Return")
      .WithDescription("Processes the VNPay return response, validates the signature, and updates the order.");
      #endregion
    }
  }
}
