using CleanArchitecture.Application.DTOs.Payment;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<List<PaymentResponse>>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _paymentRepository.GetAllAsync();

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