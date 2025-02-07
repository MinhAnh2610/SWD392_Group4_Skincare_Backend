using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.Enums;

namespace CleanArchitecture.Presentation.Endpoints
{
    public class RefundController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/refund").WithTags("Refund Management");

            group.MapGet("/", async (IRefundService refundService) =>
            {
                var result = await refundService.GetAllRefundsAsync();
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<List<RefundResponse>>.SuccessResponse(result.Data!, "Refunds Retrieved Successfully."));
                }
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            })
            .WithName("GetRefunds")
            .Produces<ApiResponse<List<RefundResponse>>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("GetRefunds")
            .WithDescription("Get All Refunds")
            .RequireAuthorization();

            group.MapGet("/{id}", async (IRefundService refundService, Guid id) =>
            {
                var result = await refundService.GetRefundByIdAsync(id);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<RefundResponse>.SuccessResponse(result.Data!, "Refund Retrieved Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<RefundResponse>.FailureResponse(result.Errors, "Refund Not Found.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("GetRefundById")
            .Produces<ApiResponse<RefundResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("GetRefundById")
            .WithDescription("Get Refund by ID")
            .RequireAuthorization();

            group.MapPost("/", async (IRefundService refundService, CreateRefundRequest request) =>
            {
                var result = await refundService.CreateRefundAsync(request);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<RefundResponse>.SuccessResponse(result.Data!, "Refund Created Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status400BadRequest => Results.BadRequest(ApiResponse<RefundResponse>.FailureResponse(result.Errors, "Invalid Input.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("CreateRefund")
            .Produces<ApiResponse<RefundResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("CreateRefund")
            .WithDescription("Create New Refund")
            .RequireAuthorization();

            group.MapPut("/", async (IRefundService refundService, UpdateRefundRequest request) =>
            {
                var result = await refundService.UpdateRefundAsync(request);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<RefundResponse>.SuccessResponse(result.Data!, "Refund Updated Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status400BadRequest => Results.BadRequest(ApiResponse<RefundResponse>.FailureResponse(result.Errors, "Invalid Input.")),
                    StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<RefundResponse>.FailureResponse(result.Errors, "Refund Not Found.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("UpdateRefund")
            .Produces<ApiResponse<RefundResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("UpdateRefund")
            .WithDescription("Update Refund")
            .RequireAuthorization();

            group.MapDelete("/{id}", async (IRefundService refundService, Guid id) =>
            {
                var result = await refundService.DeleteRefundAsync(id);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<string>.SuccessResponse(result.Data!, "Refund Deleted Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<string>.FailureResponse(result.Errors, "Refund Not Found.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("DeleteRefund")
            .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("DeleteRefund")
            .WithDescription("Delete Refund")
            .RequireAuthorization();
        }
    }
}
