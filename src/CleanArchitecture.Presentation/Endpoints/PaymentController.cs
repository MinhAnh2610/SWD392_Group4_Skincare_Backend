using CleanArchitecture.Application.DTOs.Payment;

namespace CleanArchitecture.Presentation.Endpoints;

public class PaymentController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/payment").WithTags("Payment Management");

    #region Get All Payments API
    group.MapGet("/", async (IPaymentService paymentService) =>
    {
      var result = await paymentService.GetAllPaymentsAsync();
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<List<PaymentResponse>>.SuccessResponse(result.Data!, "Retrieved Payments Successfully."));
      }

      return result.Status switch
      {
        _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
      };
    })
    .WithName("GetAllPayments")
    .Produces<ApiResponse<List<PaymentResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status401Unauthorized)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetAllPayments")
    .WithDescription("Get All Payments")
    .RequireAuthorization();
    #endregion
  }
}