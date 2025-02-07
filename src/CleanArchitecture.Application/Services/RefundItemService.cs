using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class RefundItemService : IRefundItemService
    {
        private readonly IRefundItemRepository _refundItemRepository;
        private readonly IValidator<CreateRefundItemRequest> _createValidator;
        private readonly IValidator<UpdateRefundItemRequest> _updateValidator;

        public RefundItemService(
            IRefundItemRepository refundItemRepository,
            IValidator<CreateRefundItemRequest> createValidator,
            IValidator<UpdateRefundItemRequest> updateValidator)
        {
            _refundItemRepository = refundItemRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<List<RefundItemResponse>>> GetAllRefundItemsAsync()
        {
            try
            {
                var refundItems = await _refundItemRepository.GetAllAsync();
                var response = refundItems.Select(ri => new RefundItemResponse
                {
                    Id = ri.Id,
                    Quantity = ri.Quantity,
                    Reason = ri.Reason,
                    CreateAt = ri.CreateAt,
                    CreatedBy = ri.CreatedBy,
                    LastModified = ri.LastModified,
                    LastModifiedBy = ri.LastModifiedBy
                }).ToList();

                return Result<List<RefundItemResponse>>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<List<RefundItemResponse>>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<RefundItemResponse>> GetRefundItemByIdAsync(Guid id)
        {
            try
            {
                var refundItem = await _refundItemRepository.GetByIdAsync(id);
                if (refundItem == null)
                    return Result<RefundItemResponse>.Failure(
                        new List<Error> { new Error("RefundItemNotFound", "Refund item not found.") },
                        StatusCodes.Status404NotFound);

                var response = new RefundItemResponse
                {
                    Id = refundItem.Id,
                    Quantity = refundItem.Quantity,
                    Reason = refundItem.Reason,
                    CreateAt = refundItem.CreateAt,
                    CreatedBy = refundItem.CreatedBy,
                    LastModified = refundItem.LastModified,
                    LastModifiedBy = refundItem.LastModifiedBy
                };

                return Result<RefundItemResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<RefundItemResponse>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<RefundItemResponse>> CreateRefundItemAsync(CreateRefundItemRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new Error("ValidationError", e.ErrorMessage))
                    .ToList();
                return Result<RefundItemResponse>.Failure(errors, StatusCodes.Status400BadRequest);
            }

            try
            {
                var refundItem = new RefundItem
                {
                    Quantity = request.Quantity,
                    Reason = request.Reason
                };

                await _refundItemRepository.CreateAsync(refundItem);

                var response = new RefundItemResponse
                {
                    Id = refundItem.Id,
                    Quantity = refundItem.Quantity,
                    Reason = refundItem.Reason,
                    CreateAt = refundItem.CreateAt,
                    CreatedBy = refundItem.CreatedBy,
                    LastModified = refundItem.LastModified,
                    LastModifiedBy = refundItem.LastModifiedBy
                };

                return Result<RefundItemResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<RefundItemResponse>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<RefundItemResponse>> UpdateRefundItemAsync(UpdateRefundItemRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new Error("ValidationError", e.ErrorMessage))
                    .ToList();
                return Result<RefundItemResponse>.Failure(errors, StatusCodes.Status400BadRequest);
            }

            try
            {
                var refundItem = await _refundItemRepository.GetByIdAsync(request.Id);
                if (refundItem == null)
                    return Result<RefundItemResponse>.Failure(
                        new List<Error> { new Error("RefundItemNotFound", "Refund item not found.") },
                        StatusCodes.Status404NotFound);

                refundItem.Quantity = (int)request.Quantity;
                refundItem.Reason = request.Reason;

                await _refundItemRepository.UpdateAsync(refundItem);

                var response = new RefundItemResponse
                {
                    Id = refundItem.Id,
                    Quantity = refundItem.Quantity,
                    Reason = refundItem.Reason,
                    CreateAt = refundItem.CreateAt,
                    CreatedBy = refundItem.CreatedBy,
                    LastModified = refundItem.LastModified,
                    LastModifiedBy = refundItem.LastModifiedBy
                };

                return Result<RefundItemResponse>.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return Result<RefundItemResponse>.Failure(
                    new List<Error> { new Error("ServerError", "An error occurred while processing your request.") },
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<string>> DeleteRefundItemAsync(Guid id)
        {
            try
            {
                var refundItem = await _refundItemRepository.GetByIdAsync(id);
                if (refundItem == null)
                    return Result<string>.Failure(
                        new List<Error> { new Error("RefundItemNotFound", "Refund item not found.") },
                        StatusCodes.Status404NotFound);

                //await _refundItemRepository.DeleteAsync(refundItem);

                return Result<string>.Success("Refund item deleted successfully.", StatusCodes.Status200OK);
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
