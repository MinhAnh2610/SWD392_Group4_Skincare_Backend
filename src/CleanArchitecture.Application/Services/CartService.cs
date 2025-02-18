using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
   
    
    public class CartService : ICartService
    {
      private readonly ICartRepository _cartRepository;
        
      public CartService(ICartRepository cartRepository)
      {
          _cartRepository = cartRepository;
      }
      public Task<Result<CartResponse>> AddCartItemAsync()
      {
        throw new NotImplementedException();
      }

      public Task<Result<CartResponse>> DeletebyIdAsync(Guid id)
      {
        throw new NotImplementedException();
      }

      public async Task<Result<List<CartResponse>>> GetAllCartsAsync()
      {
          try
          {
              var carts = await  _cartRepository.GetAllAsync();
              var response = carts.Select(cart => new CartResponse
              {
                  Id = cart.Id,
                  TotalPrice = cart.TotalPrice,
                  Customer = cart.Customer,
                  Items = cart.CartItems
              }).ToList();
              return Result<List<CartResponse>>.Success(response, StatusCodes.Status200OK);
          }
          catch(Exception ex)
          {
              return Result<List<CartResponse>>.Failure(
                     new List<Error> { new Error("Cart.GetAll", ex.Message) },
                     StatusCodes.Status500InternalServerError
                     );
      }
      }

      public Task<Result<CartResponse>> GetByIdAsync(Guid id)
      {
        throw new NotImplementedException();
      }
    }
}
