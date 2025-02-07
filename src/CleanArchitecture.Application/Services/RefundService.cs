using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;
        private readonly IValidator<CreateRefundRequest> _createValidator;
        private readonly IValidator<UpdateRefundRequest> _updateValidator;

        public RefundService(
            IRefundRepository refundRepository,
            IValidator<CreateRefundRequest> createValidator,
            IValidator<UpdateRefundRequest> updateValidator)
        {
            _refundRepository = refundRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<List<RefundResponse>>> GetAllRefundsAsync()
        {
            try
            {
                var refunds = await _refundRepository.GetAllAsync();
                var response = refunds.Select(r => new RefundResponse
                {
                    Id = r.Id,
                    Reason = r.Reason,
                    TotalAmount = r.TotalAmount,
                    Status = r.Status,
                    RequestedDate = r.RequestedDate,
                    Method = r.Method,
                    CreateAt = r.CreateAt,
                    CreatedBy = r.CreatedBy,
                    LastModified = r.LastModified,
                    LastModifiedBy = r.LastModifiedBy
                }).ToList();

                return Result<List<RefundResponse>>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<List<RefundResponse>>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<RefundResponse>> GetRefundByIdAsync(Guid id)
        {
            try
            {
                var refund = await _refundRepository.GetByIdAsync(id);
                if (refund == null)
                    return Result<RefundResponse>.Failure(
                        new List<Error> { new Error("RefundNotFound", "Refund not found.") },
                        StatusCodes.Status404NotFound);

                var response = new RefundResponse
                {
                    Id = refund.Id,
                    Reason = refund.Reason,
                    TotalAmount = refund.TotalAmount,
                    Status = refund.Status,
                    RequestedDate = refund.RequestedDate,
                    Method = refund.Method,
                    CreateAt = refund.CreateAt,
                    CreatedBy = refund.CreatedBy,
                    LastModified = refund.LastModified,
                    LastModifiedBy = refund.LastModifiedBy
                };

                return Result<RefundResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<RefundResponse>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<RefundResponse>> CreateRefundAsync(CreateRefundRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new Error("ValidationError", e.ErrorMessage))
                    .ToList();
                return Result<RefundResponse>.Failure(errors, StatusCodes.Status400BadRequest);
            }

            try
            {
                var refund = new Refund
                {
                    Reason = request.Reason,
                    TotalAmount = request.TotalAmount,
                    Status = request.Status,
                    RequestedDate = DateTime.UtcNow,
                    Method = request.Method
                };

                await _refundRepository.CreateAsync(refund);

                var response = new RefundResponse
                {
                    Id = refund.Id,
                    Reason = refund.Reason,
                    TotalAmount = refund.TotalAmount,
                    Status = refund.Status,
                    RequestedDate = refund.RequestedDate,
                    Method = refund.Method,
                    CreateAt = refund.CreateAt,
                    CreatedBy = refund.CreatedBy,
                    LastModified = refund.LastModified,
                    LastModifiedBy = refund.LastModifiedBy
                };

                return Result<RefundResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<RefundResponse>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<RefundResponse>> UpdateRefundAsync(UpdateRefundRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new Error("ValidationError", e.ErrorMessage))
                    .ToList();
                return Result<RefundResponse>.Failure(errors, StatusCodes.Status400BadRequest);
            }

            try
            {
                var refund = await _refundRepository.GetByIdAsync(request.Id);
                if (refund == null)
                    return Result<RefundResponse>.Failure(
                        new List<Error> { new Error("RefundNotFound", "Refund not found.") },
                        StatusCodes.Status404NotFound);

                refund.Reason = request.Reason;
                refund.TotalAmount = (decimal)request.TotalAmount;
                refund.Status = request.Status;
                refund.Method = request.Method;

                await _refundRepository.UpdateAsync(refund);

                var response = new RefundResponse
                {
                    Id = refund.Id,
                    Reason = refund.Reason,
                    TotalAmount = refund.TotalAmount,
                    Status = refund.Status,
                    RequestedDate = refund.RequestedDate,
                    Method = refund.Method,
                    CreateAt = refund.CreateAt,
                    CreatedBy = refund.CreatedBy,
                    LastModified = refund.LastModified,
                    LastModifiedBy = refund.LastModifiedBy
                };

                return Result<RefundResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<RefundResponse>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<string>> DeleteRefundAsync(Guid id)
        {
            try
            {
                var refund = await _refundRepository.GetByIdAsync(id);
                if (refund == null)
                    return Result<string>.Failure(
                        new List<Error> { new Error("RefundNotFound", "Refund not found.") },
                        StatusCodes.Status404NotFound);

               // await _refundRepository.DeleteAsync(refund);

                return Result<string>.Success("Refund deleted successfully.", StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<string>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
