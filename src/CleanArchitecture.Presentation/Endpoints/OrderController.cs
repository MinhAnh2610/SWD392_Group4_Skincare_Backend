// OrderController.cs
using CleanArchitecture.Application.DTOs.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class OrderController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/orders").WithTags("Order Management");

      // GET /orders → View All Customer Orders
      group.MapGet("/get-all-orders", async (IOrderService orderService) =>
      {
        var result = await orderService.GetAllOrdersAsync();
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(
              result.Data!,
              "Retrieved Orders Successfully."
          ));
        }
        return Results.StatusCode(result.Status);
      })
      .WithName("GetAllOrders")
      .Produces<ApiResponse<List<OrderResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .RequireAuthorization();

      // PUT /orders/{id}/update → Update Order Status
      group.MapPut("/{id:guid}/update", async (Guid id, UpdateOrderStatusRequest request, IOrderService orderService) =>
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
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .RequireAuthorization();

      // GET /customers/{id}/orders → View Customer Order History
      group.MapGet("/customers/{id:guid}/orders", async (Guid id, IOrderService orderService) =>
      {
        var result = await orderService.GetOrdersByCustomerIdAsync(id);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(
              result.Data!,
              "Retrieved Customer Order History Successfully."
          ));
        }
        return Results.StatusCode(result.Status);
      })
      .WithName("GetCustomerOrderHistory")
      .Produces<ApiResponse<List<OrderResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .RequireAuthorization();

      group.MapPost("/check-out", async (CheckOutRequest request, IOrderService orderService) =>
      {
        var result = await orderService.CheckOut(request);
        if (result.IsSuccess)
        {
          return Results.Created($"/orders/{result.Data!.Id}", ApiResponse<OrderResponse>.SuccessResponse(
                 result.Data!,
                 "Order created successfully."
                 ));
        }
        return Results.StatusCode(result.Status);
      })
      .WithName("Check out")
      .Produces<ApiResponse<List<OrderResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .RequireAuthorization();

      group.MapDelete("/{orderId}", async (Guid orderId, IOrderService orderService) =>
      {
        var result = await orderService.DeleteOrderAsync(orderId);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<string>.SuccessResponse(result.Data!, "Order deleted successfully."));
        }
        return Results.StatusCode(result.Status);
      })
   .WithName("DeleteOrder")
   .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
   .ProducesProblem(StatusCodes.Status404NotFound)
   .ProducesProblem(StatusCodes.Status500InternalServerError)
   .WithSummary("Delete Order")
   .WithDescription("Deletes an order by its identifier.")
   .RequireAuthorization();

    }

  }
}
