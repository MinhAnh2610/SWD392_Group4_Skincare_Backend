using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{


  public class CartService : ICartService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;
    public CartService(IUnitOfWork unitOfWork, IClaimsService claimsService)
    {
      _unitOfWork = unitOfWork;
      _claimsService = claimsService;

    }
    public async Task<Result<List<CartResponse>>> AddCartItemAsync(AddProductRequest addProductRequest)
    {
      var cart = await _unitOfWork.Carts.GetByIdAsync(addProductRequest.CartId);
      if (cart == null)
      {
        return Result<List<CartResponse>>.Failure(
              new List<Error> { new Error("Cart.GetAll", "Cart Not Found") },
              StatusCodes.Status500InternalServerError
              );
      }
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(addProductRequest.CosmeticId);
      if (cosmetic == null)
      {
        return Result<List<CartResponse>>.Failure(
              new List<Error> { new Error("Cart.GetAll", "Cosmetic Not Found") },
              StatusCodes.Status500InternalServerError
              );
      }

      int quantity = 0;
      if(cosmetic.Batches != null)
      {
        foreach (Batch batch in cosmetic.Batches)
        {
          quantity += batch.Quantity;
        }
      }

      if(quantity < addProductRequest.Quantity)
      {
        return Result<List<CartResponse>>.Failure(
               new List<Error> { new Error("Cart.GetAll", "Not enough Quantity") },
               StatusCodes.Status500InternalServerError
               );
      }

      var cartItem = new CartItem
      {
        CartId = addProductRequest.CartId,
        CosmeticId = addProductRequest.CosmeticId,
        Quantity = addProductRequest.Quantity
      };

      if(cart.CartItems != null)
      {
        cart.CartItems.Add(cartItem);
        var list = cart.CartItems.Select(x => new CartResponse
        {
          Id = cart.Id,
          TotalPrice = cart.TotalPrice,
          Customer = cart.Customer,
          Items = cart.CartItems
        }).ToList();
        if (list != null)
        {
          return Result<List<CartResponse>>.Success(list, StatusCodes.Status200OK);
        }
     
      }

      return Result<List<CartResponse>>.Failure(
               new List<Error> { new Error("Cart.GetAll", "Cart Item List not foud") },
               StatusCodes.Status500InternalServerError
               );
    }

    public async Task<Result<List<CartResponse>>> DeletebyIdAsync(RemoveProductRequest removeProductRequest)
    {
      var cart = await _unitOfWork.Carts.GetByIdAsync(removeProductRequest.CartId);
      if (cart == null)
      {
        return Result<List<CartResponse>>.Failure(
              new List<Error> { new Error("Cart.GetAll", "Cart Not Found") },
              StatusCodes.Status500InternalServerError
              );
      }
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(removeProductRequest.CosmeticId);
      if (cosmetic == null)
      {
        return Result<List<CartResponse>>.Failure(
              new List<Error> { new Error("Cart.GetAll", "Cosmeti Not Found") },
              StatusCodes.Status500InternalServerError
              );
      }

      var cartItem = cart.CartItems.FirstOrDefault(x => x.CosmeticId == removeProductRequest.CosmeticId);
      if (cartItem == null)
      {
        return Result<List<CartResponse>>.Failure(
               new List<Error> { new Error("Cart.GetAll", "Cart Item Not Found") },
               StatusCodes.Status500InternalServerError
               );
      }
      cart.CartItems.Remove(cartItem);
      var list = cart.CartItems.Select(x => new CartResponse
      {
        Id = cart.Id,
        TotalPrice = cart.TotalPrice,
        Customer = cart.Customer,
        Items = cart.CartItems
      }).ToList();
      if (list == null)
      {
        return Result<List<CartResponse>>.Failure(
               new List<Error> { new Error("Cart.GetAll", "Cart Item List not foud") },
               StatusCodes.Status500InternalServerError
               );
      }
      return Result<List<CartResponse>>.Success(list, StatusCodes.Status200OK);

    }

    public async Task<Result<List<CartResponse>>> GetAllCartsAsync()
    {
      try
      {
        var carts = await _unitOfWork.Carts.GetAllAsync();
        var response = carts.Select(cart => new CartResponse
        {
          Id = cart.Id,
          TotalPrice = cart.TotalPrice,
          Customer = cart.Customer,
          Items = cart.CartItems
        }).ToList();
        return Result<List<CartResponse>>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<List<CartResponse>>.Failure(
               new List<Error> { new Error("Cart.GetAll", ex.Message) },
               StatusCodes.Status500InternalServerError
               );
      }
    }

    public async Task<Result<CartResponse>> GetByIdAsync(Guid id)
    {
      try
      {
        var carts = await _unitOfWork.Carts.GetAllAsync();
        var response = carts.Select(cart => new CartResponse
        {
          Id = cart.Id,
          TotalPrice = cart.TotalPrice,
          Customer = cart.Customer,
          Items = cart.CartItems
        }).FirstOrDefault(x => x.Id == id);
        return Result<CartResponse>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<CartResponse>.Failure(
        new List<Error> { new Error("Cart.AddCartItem", "Cart not found") },
        StatusCodes.Status404NotFound
        );
      }
    }

    // CartService.cs - implementation of the new method
    public async Task<Result<CartResponse>> GetCartByUserIdAsync()
    {
      try
      {
        var userId = _claimsService.CurrentUserId;
        if (userId == Guid.Empty)
        {
          return Result<CartResponse>.Failure(
              new List<Error> { new Error("Cart.GetByUserId", "User not authenticated") },
              StatusCodes.Status401Unauthorized
          );
        }

        var carts = await _unitOfWork.Carts.GetAllAsync();
        var cart = carts.FirstOrDefault(c => c.CustomerId == userId);

        if (cart == null)
        {
          // User doesn't have a cart yet, create one
          cart = new Cart
          {
            Id = Guid.NewGuid(),
            CustomerId = userId,
            TotalPrice = 0,
            CartItems = new List<CartItem>()
          };

          await _unitOfWork.Carts.CreateAsync(cart);
          await _unitOfWork.CompleteAsync();
        }

        var response = new CartResponse
        {
          Id = cart.Id,
          TotalPrice = cart.TotalPrice,
          Customer = cart.Customer,
          Items = cart.CartItems
        };

        return Result<CartResponse>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.GetByUserId", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }
  }
}
