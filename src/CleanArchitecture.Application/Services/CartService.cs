using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.DTOs.CartDto;
using CleanArchitecture.Application.DTOs.CartItem;
using CleanArchitecture.Application.DTOs.UserDto;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Services
{
  public class CartService : ICartService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IClaimsService _claimsService;
    private readonly IErrorFactory _errorFactory;

    public CartService(
        IUnitOfWork unitOfWork,
        IClaimsService claimsService,
        UserManager<User> userManager,
        IErrorFactory errorFactory)
    {
      _unitOfWork = unitOfWork;
      _claimsService = claimsService;
      _userManager = userManager;
      _errorFactory = errorFactory;
    }

    public async Task<Result<List<CartResponse>>> AddCartItemForCurrentUserAsync(AddProductRequest addProductRequest)
    {
      // Get the current user's ID via IClaimsService.
      var userId = _claimsService.CurrentUserId;
      if (userId == Guid.Empty)
      {
        return Result<List<CartResponse>>.Failure(
            new List<Error> { new Error("Cart.AddCartItem", "User not authenticated") },
            StatusCodes.Status401Unauthorized);
      }

      // Retrieve the current user's cart; create one if it doesn't exist.
      var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
      if (cart == null)
      {
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

      // Retrieve the cosmetic (product) being added.
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(addProductRequest.CosmeticId);
      if (cosmetic == null)
      {
        return Result<List<CartResponse>>.Failure(
            new List<Error> { new Error("Cart.AddCartItem", "Cosmetic not found") },
            StatusCodes.Status500InternalServerError);
      }

      // Check available quantity (assuming cosmetic.Batches holds stock info).
      int availableQuantity = 0;
      if (cosmetic.Batches != null && cosmetic.Batches.Any())
      {
        availableQuantity = cosmetic.Batches.Sum(batch => batch.Quantity);
      }
      if (availableQuantity < addProductRequest.Quantity)
      {
        return Result<List<CartResponse>>.Failure(
            new List<Error> { new Error("Cart.AddCartItem", "Not enough quantity available") },
            StatusCodes.Status500InternalServerError);
      }

      // Check if the item is already in the cart; if so, update its quantity.
      var existingCartItem = cart.CartItems?.FirstOrDefault(ci => ci.CosmeticId == addProductRequest.CosmeticId);
      if (existingCartItem != null)
      {
        existingCartItem.Quantity += addProductRequest.Quantity;
      }
      else
      {
        var cartItem = new CartItem
        {
          CartId = cart.Id,
          CosmeticId = addProductRequest.CosmeticId,
          Quantity = addProductRequest.Quantity
        };
        cart.CartItems?.Add(cartItem);
      }

      // Recalculate the cart's total price.
      cart.TotalPrice += await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetic) * addProductRequest.Quantity;

      await _unitOfWork.CompleteAsync();

      // Map the updated cart to a DTO.
      var cartResponse = MapCartToCartResponse(cart);
      var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(cart);
      foreach (var item in cartResponse.Items)
      {
        item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
      }
      return Result<List<CartResponse>>.Success(new List<CartResponse> { cartResponse }, StatusCodes.Status200OK);
    }

    public async Task<Result<CartResponse>> DeleteCartItemForCurrentUserAsync(Guid cosmeticId)
    {
      // Get current user's ID
      var userId = _claimsService.CurrentUserId;
      if (userId == Guid.Empty)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.DeleteCartItem", "User not authenticated") },
            StatusCodes.Status401Unauthorized);
      }

      // Get the user's cart
      var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
      if (cart == null)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.DeleteCartItem", "Cart Not Found") },
            StatusCodes.Status404NotFound);
      }

      var cartItem = cart.CartItems?.FirstOrDefault(x => x.CosmeticId == cosmeticId);
      if (cartItem == null)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.DeleteCartItem", "Cart Item Not Found") },
            StatusCodes.Status404NotFound);
      }

      // Deduct the item price from the total before removing it
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(cartItem.CosmeticId);
      if (cosmetic != null)
      {
        cart.TotalPrice -= await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetic) * cartItem.Quantity;
      }

      cart.CartItems?.Remove(cartItem);
      await _unitOfWork.CompleteAsync();

      var cartResponse = MapCartToCartResponse(cart);
      var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(cart);
      foreach (var item in cartResponse.Items)
      {
        item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
      }
      return Result<CartResponse>.Success(cartResponse, StatusCodes.Status200OK);
    }

    public async Task<Result<List<CartResponse>>> DeletebyIdAsync(RemoveProductRequest removeProductRequest)
    {
      var cart = await _unitOfWork.Carts.GetByIdAsync(removeProductRequest.CartId);
      if (cart == null)
      {
        return Result<List<CartResponse>>.Failure(
              new List<Error> { new Error("Cart.DeleteCartItem", "Cart Not Found") },
              StatusCodes.Status500InternalServerError
        );
      }

      var cartItem = cart.CartItems?.FirstOrDefault(x => x.CosmeticId == removeProductRequest.CosmeticId);
      if (cartItem == null)
      {
        return Result<List<CartResponse>>.Failure(
               new List<Error> { new Error("Cart.DeleteCartItem", "Cart Item Not Found") },
               StatusCodes.Status500InternalServerError
        );
      }

      // Deduct the item price from the total before removing it
      var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(cartItem.CosmeticId);
      if (cosmetic != null)
      {
        cart.TotalPrice -= await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetic) * cartItem.Quantity;
      }

      cart.CartItems?.Remove(cartItem);
      await _unitOfWork.CompleteAsync();

      var cartResponse = MapCartToCartResponse(cart);
      var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(cart);
      foreach (var item in cartResponse.Items)
      {
        item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
      }
      return Result<List<CartResponse>>.Success(new List<CartResponse> { cartResponse }, StatusCodes.Status200OK);
    }

    public async Task<Result<List<CartResponse>>> GetAllCartsAsync()
    {
      try
      {
        var carts = await _unitOfWork.Carts.GetAllAsync();
        var response = carts.Select(cart => MapCartToCartResponse(cart)).ToList();
        foreach (var cart in response)
        {
          var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(carts.First(c => c.Id == cart.Id));
          foreach (var item in cart.Items)
          {
            item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
          }
        }
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
        var cart = await _unitOfWork.Carts.GetByIdAsync(id);
        if (cart == null)
        {
          return Result<CartResponse>.Failure(
              new List<Error> { new Error("Cart.GetById", "Cart not found") },
              StatusCodes.Status404NotFound
          );
        }
        var response = MapCartToCartResponse(cart);
        var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(cart);
        foreach (var item in response.Items)
        {
          item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
        }
        return Result<CartResponse>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.GetById", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }

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

        // Get the cart for the current user (if it exists)
        var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);

        // If the user doesn't have a cart, create one
        if (cart == null)
        {
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

        var response = MapCartToCartResponse(cart);
        var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(cart!);
        foreach (var item in response.Items)
        {
          item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
        }
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

    public async Task<Result<CartResponse>> UpdateCartAsync(UpdateCartRequest request)
    {
      // Removed validation logic
      var cart = await _unitOfWork.Carts.GetByIdAsync(request.CartId);
      if (cart == null)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.UpdateCart", "Cart Not Found") },
            StatusCodes.Status404NotFound
        );
      }

      // Update cart logic
      return await UpdateCartItems(cart, request.Items);
    }

    public async Task<Result<CartResponse>> UpdateCurrentUserCartAsync(List<UpdateCartItemDto> items)
    {
      // Removed validation logic
      var userId = _claimsService.CurrentUserId;
      if (userId == Guid.Empty)
      {
        return Result<CartResponse>.Failure(
            new List<Error> { new Error("Cart.UpdateCart", "User not authenticated") },
            StatusCodes.Status401Unauthorized
        );
      }

      // Get or create user's cart
      var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
      if (cart == null)
      {
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

      // Update cart logic
      return await UpdateCartItems(cart, items);
    }

    // Helper method for updating cart items (used by both UpdateCartAsync and UpdateCurrentUserCartAsync)
    private async Task<Result<CartResponse>> UpdateCartItems(Cart cart, List<UpdateCartItemDto> items)
    {
      cart.CartItems?.Clear();
      cart.TotalPrice = 0;

      foreach (var item in items)
      {
        var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(item.CosmeticId);
        if (cosmetic == null)
        {
          return Result<CartResponse>.Failure(
              new List<Error> { new Error("Cart.UpdateCart", $"Cosmetic with ID {item.CosmeticId} Not Found") },
              StatusCodes.Status400BadRequest
          );
        }

        int availableQuantity = cosmetic.Batches?.Sum(b => b.Quantity) ?? 0;
        if (item.Quantity > availableQuantity)
        {
          return Result<CartResponse>.Failure(
              new List<Error> { new Error("Cart.UpdateCart", "Not enough quantity available for one or more items") },
              StatusCodes.Status400BadRequest
          );
        }

        var cartItem = new CartItem
        {
          CartId = cart.Id,
          CosmeticId = item.CosmeticId,
          Quantity = item.Quantity
        };

        cart.CartItems?.Add(cartItem);
        cart.TotalPrice += await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetic) * item.Quantity;
      }

      await _unitOfWork.CompleteAsync();

      var existingCart = await _unitOfWork.Carts.GetByIdAsync(cart.Id);
      var cosmetics = await _unitOfWork.Cosmetics.GetCosmeticsByCart(existingCart!);
      var cartResponse = MapCartToCartResponse(existingCart!);
      foreach (var item in cartResponse.Items)
      {
        item.Price = await _unitOfWork.Cosmetics.GetCosmeticPrice(cosmetics.First(c => c.Id == item.CosmeticId));
      }
      return Result<CartResponse>.Success(cartResponse, StatusCodes.Status200OK);
    }

    // Helper method to map a Cart (domain entity) to CartResponse (DTO)
    private CartResponse MapCartToCartResponse(Cart cart)
    {
      var response = new CartResponse
      {
        Id = cart.Id,
        Customer = new CustomerDto
        {
          Id = cart.CustomerId,
          UserName = cart.Customer?.FirstName ?? string.Empty,
          Email = cart.Customer?.Email ?? string.Empty
        },
        Items = cart.CartItems!.Select(ci => new CartItemDto
        {
          CosmeticId = ci.CosmeticId,
          CosmeticName = ci.Cosmetic?.Name ?? string.Empty,
          // Select only the first image URL to avoid circular references:
          CosmeticImage = ci.Cosmetic?.CosmeticImages.FirstOrDefault()?.ImageUrl ?? string.Empty,
          // Price will be set later
          Quantity = ci.Quantity,
          Height = ci.Cosmetic?.Height ?? 0,
          Length = ci.Cosmetic?.Length ?? 0,
          Weight = ci.Cosmetic?.Weight ?? 0,
          Width = ci.Cosmetic?.Width ?? 0
        }).ToList()
      };

      return response;
    }
  }
}