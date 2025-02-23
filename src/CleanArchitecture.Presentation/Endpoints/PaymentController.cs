using Carter;
using CleanArchitecture.Application.DTOs.Payment;
using CleanArchitecture.Application.DTOs.VnPay;
using CleanArchitecture.Application.ServiceContracts;
using Microsoft.AspNetCore.Http;
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
      group.MapPost("/create-payment", async (IVnPayIntegrationService vnPayIntegrationService, HttpContext context, VnPayPaymentRequestDto request) =>
      {
        try
        {
          // Call the integration service to create the payment URL.
          var paymentUrl = vnPayIntegrationService.CreatePaymentUrl(request, context);
          var result = Result<string>.Success(paymentUrl, StatusCodes.Status200OK);
          return result.Match("Payment URL created successfully.");
        }
        catch (System.Exception ex)
        {
          var errors = new List<Error> { new Error("Payment.CreatePayment", ex.Message) };
          var result = Result<string>.Failure(errors, StatusCodes.Status400BadRequest);
          return result.Match("Error creating payment URL.");
        }
      })
      .WithName("CreatePaymentUrl")
      .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Payment URL")
      .WithDescription("Creates a payment URL for VNPay integration.");
      #endregion

      #region Process VNPay Return Endpoint
      group.MapGet("/vnpay-return", async (IVnPayIntegrationService vnPayIntegrationService, HttpContext context) =>
      {
        try
        {
          // Process the return callback from VNPay.
          var response = await vnPayIntegrationService.ProcessReturnAsync(context.Request.Query);
          var result = Result<VnPayPaymentResponseDto>.Success(response, StatusCodes.Status200OK);
          return result.Match("Payment processed successfully.");
        }
        catch (System.Exception ex)
        {
          var errors = new List<Error> { new Error("Payment.VnPayReturn", ex.Message) };
          var result = Result<VnPayPaymentResponseDto>.Failure(errors, StatusCodes.Status400BadRequest);
          return result.Match("Error processing payment return.");
        }
      })
      .WithName("ProcessVnPayReturn")
      .Produces<ApiResponse<VnPayPaymentResponseDto>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Process VNPay Return")
      .WithDescription("Processes the VNPay return response, validates the signature, and stores the payment record.");
      #endregion
    }
  }
}
