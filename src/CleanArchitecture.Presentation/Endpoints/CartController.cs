
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
      group.MapGet("/carts", async (ICartService cartService) =>
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

      #region Add to Cart API
      group.MapPut("/add-product", async (ICartService cartService, AddProductRequest addProductRequest) =>
      {
        var result = await cartService.AddCartItemAsync(addProductRequest);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<CartResponse>>.SuccessResponse(result.Data!, "Add Item to Cart Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("AddProductToCart")
      .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("AddProductToCart")
      .WithDescription("Add ProductTo Cart")
      .RequireAuthorization();
      #endregion

      #region Delete Item from Cart API
      group.MapPut("/delete-product", async (ICartService cartService, RemoveProductRequest removeProductRequest) =>
      {
        var result = await cartService.DeletebyIdAsync(removeProductRequest);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<CartResponse>>.SuccessResponse(result.Data!, "Delete From Cart Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("DeleteProductFromCart")
      .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("DeleteProductFromCart")
      .WithDescription("Delete Product From Cartt")
      .RequireAuthorization();
      #endregion

      #region View Cart from Cart API
      group.MapGet("/view-cart", async (ICartService cartService, Guid id) =>
      {
        var result = await cartService.GetByIdAsync(id);
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<CartResponse>.SuccessResponse(result.Data!, "Delete From Cart Successfully."));
        }

        return result.Status switch
        {
          _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
      })
      .WithName("ViewCart")
      .Produces<ApiResponse<List<CartResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .WithSummary("ViewCart")
      .WithDescription("View Cart")
      .RequireAuthorization();
      #endregion
    }
  }
}
