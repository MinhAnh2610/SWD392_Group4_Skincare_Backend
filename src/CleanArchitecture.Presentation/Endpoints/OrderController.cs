using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.Order;
using CleanArchitecture.Application.DTOs.OrderDto;
using CleanArchitecture.Application.DTOs.VnPay;
using Microsoft.AspNetCore.Mvc;

public class OrderController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/orders").WithTags("Order Management");

    // 1. Create Order
    group.MapPost("/", async (
        [FromBody] CreateOnlineOrderRequest request,
        IOrderService orderService,
        [FromQuery] string? orderType = "online")=>
    {
      var result = await orderService.InitiateOrder(request);

      return result.Match(Message.SUCCESSFUL_CREATED(nameof(result)));
    })
    .WithName("CreateOrder")
    .Produces<ApiResponse<OrderResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("CreateOrder")
    .WithDescription("Create Order");

    group.MapPost("/walkin", async ([FromBody] CreateWalkInOrderRequest request, IOrderService orderService) =>
    {
      var result = await orderService.InitiateOrder(request);

      return result.Match(Message.SUCCESSFUL_CREATED(nameof(result)));
    })
    .WithName("CreateWalkInOrder")
    .Produces<ApiResponse<OrderResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("CreateWalkInOrder")
    .WithDescription("Create Walk-In Order");
    
    // 2. Complete Order (After payment)
    group.MapPost("/{orderId}/complete", async (
        Guid orderId,
        [FromQuery] string paymentStatus,
        [FromBody] PaymentReturnData paymentData,
        IOrderService orderService) =>
    {
      var result = await orderService.CompleteOrder(orderId, paymentStatus, paymentData);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<OrderResponse>.SuccessResponse(
            result.Data!,
            "Order completed successfully."
        ));
      }
      return Results.StatusCode(result.Status);
    })
    .WithName("CompleteOrder")
    .Produces<ApiResponse<OrderResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("CompleteOrder")
    .WithDescription("Complete Order")
    .RequireAuthorization();

    // 3. Get All Orders (Admin)
    group.MapGet("/", async (IOrderService orderService) =>
    {
      var result = await orderService.GetAllOrdersAsync();
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(
            result.Data!,
            "Retrieved orders successfully."
        ));
      }
      return Results.StatusCode(result.Status);
    })
    .WithName("GetAllOrders")
    .Produces<ApiResponse<List<OrderResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetAllOrders")
    .WithDescription("Get All Order")
    .RequireAuthorization(new AuthorizeAttribute
    {
      Roles = "Manager"
    });

    // 4. Get Orders for the Current User
    group.MapGet("/my", async (IOrderService orderService, IClaimsService claimsService) =>
    {
      var result = await orderService.GetOrdersByCustomerIdAsync(claimsService.CurrentUserId);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(
            result.Data!,
            "Retrieved customer orders successfully."
        ));
      }
      return Results.StatusCode(result.Status);
    })
    .WithName("GetMyOrders")
    .Produces<ApiResponse<List<OrderResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetMyOrders")
    .WithDescription("Get My Orders")
    .RequireAuthorization();

    // 5. Update Order Status (Admin)
    group.MapPut("/{id}/status", async (
        Guid id,
        [FromBody] UpdateOrderStatusRequest request,
        IOrderService orderService) =>
    {
      var result = await orderService.UpdateOrderStatusAsync(id, request);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<OrderResponse>.SuccessResponse(
            result.Data!,
            "Order status updated successfully."
        ));
      }
      return Results.StatusCode(result.Status);
    })
    .WithName("UpdateOrderStatus")
    .Produces<ApiResponse<OrderResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("Update Order Status")
    .WithDescription("Update Order Status")
    .RequireAuthorization(new AuthorizeAttribute
    {
      Roles = "Manager"
    });

    // 6. Delete Order (Admin)
    group.MapDelete("/{orderId}", async (
        Guid orderId,
        IOrderService orderService) =>
    {
      var result = await orderService.DeleteOrderAsync(orderId);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<string>.SuccessResponse(
            result.Data!,
            "Order deleted successfully."
        ));
      }
      return Results.StatusCode(result.Status);
    })
    .WithName("DeleteOrder")
    .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("DeleteOrder")
    .WithDescription("Delete Order")
    .RequireAuthorization(new AuthorizeAttribute
    {
      Roles = "Manager"
    });
  }
}
