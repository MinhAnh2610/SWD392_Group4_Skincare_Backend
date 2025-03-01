
using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.DTOs.Payment;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class CartController : ICarterModule
  {
    void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
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

      #region Get Cart By Id
      // GET cart by ID
      group.MapGet("/{id}", async (ICartService cartService, Guid id) =>
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
      #endregion

      #region Add to Cart API
      group.MapPut("/{cartId}/items", async (ICartService cartService, Guid cartId, AddProductRequest addProductRequest) =>
      {
        // Ensure cartId from path is used
        addProductRequest.CartId = cartId;
        var result = await cartService.AddCartItemAsync(addProductRequest);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<CartResponse>>.SuccessResponse(result.Data!, "Item added to cart successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
        .WithName("AddCartItem")
        .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("AddCartItem")
        .WithDescription("Add Item to Cart")
        .RequireAuthorization();
      #endregion 

      #region Delete Item from Cart API
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
  .WithDescription("Remove Item from Cart")
  .RequireAuthorization();
      #endregion


      #region Get current user's cart
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
