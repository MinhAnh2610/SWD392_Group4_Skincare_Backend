using CleanArchitecture.Application.DTOs.GHN.Request;
using CleanArchitecture.Application.DTOs.GHN.Response;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints;

public class GHNController : CarterModule
{
  public override void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/ghn").WithTags("Delivery Service Management");

    #region Create Shipping Order API
    group.MapPost("/create-shipping-order", async (IGHNService service, [FromBody] CreateGHNOrderRequest request) =>
    {
      var result = await service.CreateShippingOrderAsync(request);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("CreateShippingOrder")
    .Produces<ApiResponse<ShippingFeeData>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CreateShippingOrder")
    .WithDescription("Create Shipping Order");
    #endregion

    #region Calculate Fee API
    group.MapPost("/calculate-fee", async (IGHNService service, [FromBody] CalculateShippingFeeRequest request) =>
    {
      var result = await service.GetShippingFeeAsync(request!);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("CalculateFee")
    .Produces<ApiResponse<FeeData>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("CalculateFee")
    .WithDescription("Calculate Shipping Order Fee");
    #endregion

    #region Track Order API
    group.MapGet("/track-order", async (IGHNService service, [FromBody] GetShippingOrderRequest request) =>
    {
      var result = await service.GetOrderTrackingAsync(request);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("TrackOrder")
    .Produces<ApiResponse<ShippingOrderData>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("TrackOrder")
    .WithDescription("Track Shipping Order");
    #endregion

    #region Change Shipping Order Status API
    group.MapPost("/create-return-order", async (IGHNService service, [FromBody] SwitchShippingOrdersStatusRequest request, [FromQuery] string status) =>
    {
      var result = await service.ChangeShippingOrderStatus(request, status);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("ChangeShippingOrderStatus")
    .Produces<ApiResponse<ShippingOrderStatus>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("ChangeShippingOrderStatus")
    .WithDescription("Change Shipping Order Status");
    #endregion

    #region Get Store Info API
    group.MapPost("/get-store-info", async (IGHNService service, HttpContext context) =>
    {
      var requestData = await context.Request.ReadFromJsonAsync<object>();
      var result = await service.GetStoreInformationAsync(requestData!);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetStoreInfo")
    .Produces<ApiResponse<StoreData>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetStoreInfo")
    .WithDescription("Get Store Information");
    #endregion

    #region Get District API
    group.MapPost("/get-district", async (IGHNService service, [FromBody] GetDistrictRequest request) =>
    {
      var result = await service.GetDistrictAsync(request);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetDistrict")
    .Produces<ApiResponse<List<DistrictData>>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetDistrict")
    .WithDescription("Get District");
    #endregion

    #region Get Ward API
    group.MapPost("/get-ward", async (IGHNService service, [FromBody] GetWardRequest request) =>
    {
      var result = await service.GetWardAsync(request);

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetWard")
    .Produces<ApiResponse<List<WardData>>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetWard")
    .WithDescription("Get Ward");
    #endregion

    #region Get Province API
    group.MapGet("/get-province", async (IGHNService service) =>
    {
      var result = await service.GetProvinceAsync();

      return result.Match(Message.SUCCESSFUL_RETRIEVED(nameof(result)));
    })
    .WithName("GetProvince")
    .Produces<ApiResponse<List<ProvinceData>>>()
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetProvince")
    .WithDescription("Get Province");
    #endregion
  }
}
