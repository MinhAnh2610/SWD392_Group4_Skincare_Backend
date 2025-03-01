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

    group.MapPost("/get-store-info", async (IGHNService service, HttpContext context) =>
    {
      var requestData = await context.Request.ReadFromJsonAsync<object>();
      var result = await service.GetStoreInformationAsync(requestData!);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetStoreInfo")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetStoreInfo")
    .WithDescription("Get Store Information");

    group.MapGet("/get-district", async (IGHNService service, int provinceId) =>
    {
      var result = await service.GetDistrictAsync(provinceId);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetDistrict")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetDistrict")
    .WithDescription("Get District");

    group.MapPost("/get-ward", async (IGHNService service, int districtId) =>
    {
      var result = await service.GetWardAsync(districtId);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetWard")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetWard")
    .WithDescription("Get Ward");

    group.MapGet("/get-province", async (IGHNService service) =>
    {
      var result = await service.GetProvinceAsync();

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetProvince")
    .Produces<ApiResponse<string>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetProvince")
    .WithDescription("Get Province");
  }
}
