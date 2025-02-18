
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
      group.MapGet("/add-product", async (ICartService cartService) =>
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
    }
  }
}
