using CleanArchitecture.Application.DTOs.Payment;
using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Domain.RepositoryContracts.Base;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IValidator<CreatePaymentRequest> _createValidator;
        private readonly IValidator<UpdatePaymentRequest> _updateValidator;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IValidator<CreatePaymentRequest> createValidator,
            IValidator<UpdatePaymentRequest> updateValidator)
        {
            _paymentRepository = paymentRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
                    Status = p.Status,
                    Date = p.Date,
                    Method = p.Method,
                    CreateAt = p.CreateAt,
                    CreatedBy = p.CreatedBy,
                    LastModified = p.LastModified,
                    LastModifiedBy = p.LastModifiedBy
                }).ToList();

                return Result<List<PaymentResponse>>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<List<PaymentResponse>>.Failure(new List<Error> { new Error("ServerError", "An error occurred while processing your request.") }, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<PaymentResponse>> GetPaymentByIdAsync(Guid id)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);
                if (payment == null)
                    return Result<PaymentResponse>.Failure(new List<Error> { new Error("PaymentNotFound", "Payment not found.") }, StatusCodes.Status404NotFound);

                var response = new PaymentResponse
                {
                    Id = payment.Id,
                    TotalAmount = payment.TotalAmount,
                    Status = payment.Status,
                    Date = payment.Date,
                    Method = payment.Method,
                    CreateAt = payment.CreateAt,
                    CreatedBy = payment.CreatedBy,
                    LastModified = payment.LastModified,
                    LastModifiedBy = payment.LastModifiedBy
                };

                return Result<PaymentResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<PaymentResponse>.Failure(new List<Error> { new Error("ServerError", "An error occurred while processing your request.") }, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<PaymentResponse>> CreatePaymentAsync(CreatePaymentRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new Error("ValidationError", e.ErrorMessage))
                    .ToList();
                return Result<PaymentResponse>.Failure(errors, StatusCodes.Status400BadRequest);
            }

            try
            {
                var payment = new Payment
                {
                    TotalAmount = request.TotalAmount,
                    Status = request.Status,
                   Date = DateTime.UtcNow,
                    Method = request.Method
                };

                await _paymentRepository.CreateAsync(payment);

                var response = new PaymentResponse
                {
                    Id = payment.Id,
                    TotalAmount = payment.TotalAmount,
                    Status = payment.Status,
                    Date = payment.Date,
                    Method = payment.Method,
                    CreateAt = payment.CreateAt,
                    CreatedBy = payment.CreatedBy,
                    LastModified = payment.LastModified,
                    LastModifiedBy = payment.LastModifiedBy
                };

                return Result<PaymentResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<PaymentResponse>.Failure(new List<Error> { new Error("ServerError", "An error occurred while processing your request.") }, StatusCodes.Status500InternalServerError);
            }
        }

        public Task<Result<PaymentResponse>> UpdatePaymentAsync(UpdatePaymentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> DeletePaymentAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}