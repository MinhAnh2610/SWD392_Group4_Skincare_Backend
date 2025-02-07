using CleanArchitecture.Application.DTOs.Payment;
using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.Enums;

namespace CleanArchitecture.Presentation.Endpoints;

public class PaymentController : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/payment").WithTags("Payment Management");

        group.MapGet("/", async (IPaymentService paymentService) =>
        {
            var result = await paymentService.GetAllPaymentsAsync();
            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<List<PaymentResponse>>.SuccessResponse(result.Data!, "Payments Retrieved Successfully."));
            }

            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        })
        .WithName("GetPayments")
        .Produces<ApiResponse<List<PaymentResponse>>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("GetPayments")
        .WithDescription("Get All Payments")
        .RequireAuthorization();

        group.MapGet("/{id}", async (IPaymentService paymentService, Guid id) =>
        {
            var result = await paymentService.GetPaymentByIdAsync(id);
            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<PaymentResponse>.SuccessResponse(result.Data!, "Payment Retrieved Successfully."));
            }

            return result.Status switch
            {
                StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<PaymentResponse>.FailureResponse(result.Errors, "Payment Not Found.")),
                _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
            };
        })
        .WithName("GetPaymentById")
        .Produces<ApiResponse<PaymentResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("GetPaymentById")
        .WithDescription("Get Payment by ID")
        .RequireAuthorization();

        group.MapPost("/", async (IPaymentService paymentService, CreatePaymentRequest request) =>
        {
            var result = await paymentService.CreatePaymentAsync(request);
            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<PaymentResponse>.SuccessResponse(result.Data!, "Payment Created Successfully."));
            }

            return result.Status switch
            {
                StatusCodes.Status400BadRequest => Results.BadRequest(ApiResponse<PaymentResponse>.FailureResponse(result.Errors, "Invalid Input.")),
                _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
            };
        })
        .WithName("CreatePayment")
        .Produces<ApiResponse<PaymentResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("CreatePayment")
        .WithDescription("Create New Payment")
        .RequireAuthorization();
    }
}