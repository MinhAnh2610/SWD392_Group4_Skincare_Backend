using CleanArchitecture.Application.DTOs.Payment;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
  public class PaymentService : IPaymentService
  {
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<PaymentResponse>>> GetAllPaymentsAsync()
    {
      try
      {
        var payments = await _unitOfWork.Payments.GetAllAsync();

        var response = payments.Select(p => new PaymentResponse
        {
          Id = p.Id,
          TotalAmount = p.TotalAmount,
          Date = p.Date,
          Method = p.Method,
          CreateAt = p.CreateAt,
          CreatedBy = p.CreatedBy,
          LastModified = p.LastModified,
          LastModifiedBy = p.LastModifiedBy
        }).ToList();

        return Result<List<PaymentResponse>>.Success(response, StatusCodes.Status200OK);
      }
      catch (Exception ex)
      {
        return Result<List<PaymentResponse>>.Failure(
            new List<Error> { new Error("Payment.GetAll", ex.Message) },
            StatusCodes.Status500InternalServerError
        );
      }
    }
  }
}