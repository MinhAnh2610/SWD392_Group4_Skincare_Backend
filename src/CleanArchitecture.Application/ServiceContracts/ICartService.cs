using CleanArchitecture.Application.DTOs.Cart;
using CleanArchitecture.Application.DTOs.CartDto;

namespace CleanArchitecture.Application.Interfaces
{
  public interface ICartService
  {
    Task<Result<List<CartResponse>>> GetAllCartsAsync();
    Task<Result<CartResponse>> GetByIdAsync(Guid id);
    Task<Result<CartResponse>> GetCartByUserIdAsync();
    Task<Result<List<CartResponse>>> AddCartItemForCurrentUserAsync(AddProductRequest addProductRequest);
    Task<Result<CartResponse>> DeleteCartItemForCurrentUserAsync(Guid cosmeticId);
    Task<Result<List<CartResponse>>> DeletebyIdAsync(RemoveProductRequest removeProductRequest);
    Task<Result<CartResponse>> UpdateCartAsync(UpdateCartRequest request);
    Task<Result<CartResponse>> UpdateCurrentUserCartAsync(List<UpdateCartItemDto> items);
  }
}