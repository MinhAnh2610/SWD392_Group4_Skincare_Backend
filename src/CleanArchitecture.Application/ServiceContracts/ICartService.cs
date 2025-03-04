using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.DTOs.CartDto;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface ICartService
  {
    Task<Result<List<CartResponse>>> GetAllCartsAsync();
    Task<Result<List<CartResponse>>> AddCartItemForCurrentUserAsync(AddProductRequest addProductRequest);
    Task<Result<CartResponse>> GetByIdAsync(Guid id);
    Task<Result<List<CartResponse>>> DeletebyIdAsync(RemoveProductRequest removeProductRequest);
    Task<Result<CartResponse>> GetCartByUserIdAsync();
    Task<Result<CartResponse>> UpdateCartAsync(UpdateCartRequest request);
  }
}
