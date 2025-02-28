namespace CleanArchitecture.Presentation.Endpoints;

public class GHNController : CarterModule
{
  public override void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/ghn").WithTags("Delivery Service Management");

    group.MapPost("/create-shipping-order", async (IGHNService service, HttpContext context) =>
    {
      var requestData = await context.Request.ReadFromJsonAsync<object>();
      var result = await service.CreateShippingOrderAsync(requestData!);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("CreateShippingOrder")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CreateShippingOrder")
    .WithDescription("Create Shipping Order");

    group.MapPost("/calculate-fee", async (IGHNService service, HttpContext context) =>
    {
      var requestData = await context.Request.ReadFromJsonAsync<object>();
      var result = await service.GetShippingFeeAsync(requestData!);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("CalculateFee")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CalculateFee")
    .WithDescription("Calculate Shipping Order Fee");

    group.MapGet("/track-order/{orderCode}", async (IGHNService service, string orderCode) =>
    {
      var result = await service.GetOrderTrackingAsync(orderCode);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("TrackOrder")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("TrackOrder")
    .WithDescription("Track Shipping Order");

    group.MapPost("/create-return-order", async (IGHNService service, HttpContext context) =>
    {
      var requestData = await context.Request.ReadFromJsonAsync<object>();
      var result = await service.CreateReturnOrderAsync(requestData!);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("CreateReturnOrder")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CreateReturnOrder")
    .WithDescription("Create Return Shipping Order");
  }
}
