using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.DTOs.CartDto;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class CartController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/cart").WithTags("Cart Management");

      #region Get All Cart API
      group.MapGet("/", async (ICartService cartService) =>
      {
        var result = await cartService.GetAllCartsAsync();
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<CartResponse>>.SuccessResponse(result.Data!, "Retrieved Carts Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("GetAllCarts")
      .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("GetAllCarts")
      .WithDescription("Get All Carts")
      .RequireAuthorization();
      #endregion

      group.MapGet("/{id:guid}", async (ICartService cartService, Guid id) =>
      {
        var result = await cartService.GetByIdAsync(id);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CartResponse>.SuccessResponse(result.Data!, "Retrieved Cart Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("GetCartById")
      .Produces<ApiResponse<CartResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("GetCartById")
      .WithDescription("Get Cart by ID")
      .RequireAuthorization();

      #region Update Cart (Current User) API
      // For current user operations, we don't need cartId in the request
      group.MapPut("/me", async (ICartService cartService, [FromBody] List<UpdateCartItemDto> items) =>
      {
        var result = await cartService.UpdateCurrentUserCartAsync(items);
        return result.Match("Updated cart successfully.");
      })
     .WithName("UpdateCartForCurrentUser")
     .Produces<ApiResponse<CartResponse>>(StatusCodes.Status200OK)
     .ProducesProblem(StatusCodes.Status400BadRequest)
     .ProducesProblem(StatusCodes.Status401Unauthorized)
     .ProducesProblem(StatusCodes.Status500InternalServerError)
     .WithSummary("UpdateCartForCurrentUser")
     .WithDescription("Update user's current cart")
     .RequireAuthorization();
      #endregion

      #region Add to Cart (Current User) API
      group.MapPut("/me/items", async (ICartService cartService, [FromBody] AddProductRequest addProductRequest) =>
      {
        var result = await cartService.AddCartItemForCurrentUserAsync(addProductRequest);
        return result.Match("Item added to cart successfully.");
      })
     .WithName("AddCartItemForCurrentUser")
     .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
     .ProducesProblem(StatusCodes.Status401Unauthorized)
     .ProducesProblem(StatusCodes.Status500InternalServerError)
     .WithSummary("AddCartItem")
     .WithDescription("Add item to current user's cart")
     .RequireAuthorization();
      #endregion

      #region Delete Item from Cart API
      // For removing items, we'll create a current user version
      group.MapDelete("/me/items/{cosmeticId}", async (ICartService cartService, Guid cosmeticId) =>
      {
        var result = await cartService.DeleteCartItemForCurrentUserAsync(cosmeticId);
        return result.Match("Item removed from cart successfully.");
      })
      .WithName("RemoveCartItemForCurrentUser")
      .Produces<ApiResponse<CartResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("RemoveCartItemForCurrentUser")
      .WithDescription("Remove item from current user's cart")
      .RequireAuthorization();

      // Keep the original endpoint for admin operations
      group.MapDelete("/{cartId}/items/{cosmeticId}", async (ICartService cartService, Guid cartId, Guid cosmeticId) =>
      {
        var request = new RemoveProductRequest { CartId = cartId, CosmeticId = cosmeticId };
        var result = await cartService.DeletebyIdAsync(request);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<CartResponse>>.SuccessResponse(result.Data!, "Item removed from cart successfully."));
        }
        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("RemoveCartItem")
      .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("RemoveCartItem")
      .WithDescription("Remove item from cart")
      .RequireAuthorization();
      #endregion

      #region Get Current User's Cart API
      group.MapGet("/me", async (ICartService cartService) =>
      {
        var result = await cartService.GetCartByUserIdAsync();
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CartResponse>.SuccessResponse(result.Data!, "Retrieved User Cart Successfully."));
        }
        return result.Status switch
        {
          StatusCodes.Status401Unauthorized => Results.Unauthorized(),
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("GetCurrentUserCart")
      .Produces<ApiResponse<CartResponse>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("GetCurrentUserCart")
      .WithDescription("Get Cart for Current User")
      .RequireAuthorization();
      #endregion
    }
  }
}