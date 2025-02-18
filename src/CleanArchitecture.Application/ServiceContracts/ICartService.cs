using CleanArchitecture.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface ICartService
  {
    Task<Result<List<CartResponse>>> GetAllCartsAsync();
    Task<Result<CartResponse>> AddCartItemAsync();
    Task<Result<CartResponse>> GetByIdAsync(Guid id);
    Task<Result<CartResponse>> DeletebyIdAsync(Guid id);
  }
}
