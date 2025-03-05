using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class RefundService : IRefundService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IClaimsService _claimsService;
  public RefundService(IUnitOfWork unitOfWork, IErrorFactory errorFactory, IClaimsService claimsService)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _claimsService = claimsService;
  }

  public async Task<Result<RefundResponse>> CreateRefundAsync(CreateRefundRequest request)
  {
    var customerId = _claimsService.CurrentUserId;
    if (customerId == Guid.Empty)
    {
      return Result<RefundResponse>.Failure(
          new List<Error> { new Error("Refund.Create", "User not authenticated") },
          StatusCodes.Status401Unauthorized);
    }

    var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
    if (order == null)
    {
      var errors = _errorFactory.CreateNotFoundError(nameof(order));
      return Result<RefundResponse>.Failure([errors.err], errors.statusCode);
    }
    if (order.Status != OrderStatus.COMPLETED)
    {
      return Result<RefundResponse>.Failure(new List<Error>
      {
        new Error("Order.Status", "Order is not completed")
      }, StatusCodes.Status400BadRequest);
    }
    if (order.CustomerId != customerId)
    {
      return Result<RefundResponse>.Failure(new List<Error>
      {
        new Error("Order.CustomerId", "Order is not from this customer")
      }, StatusCodes.Status400BadRequest);
    }

    var refund = new Refund
    {
      Id = Guid.NewGuid(),
      OrderId = request.OrderId,
      CustomerId = customerId,
      Reason = request.Reason,
      Method = request.Method,
      Status = RefundStatus.PENDING,
      RefundItems = request.RefundItems.Select(ri => new RefundItem
      {
        Id = Guid.NewGuid(),
        CosmeticId = ri.CosmeticId,
        Quantity = ri.Quantity
      }).ToList()
    };

    throw new NotImplementedException();
  }

  public Task<Result<RefundResponse>> ProcessRefundAsync(ProcessRefundRequest request)
  {
    throw new NotImplementedException();
  }

  public Task<Result<RefundResponse>> ReviewRefundAsync(ReviewRefundRequest request)
  {
    throw new NotImplementedException();
  }
}
