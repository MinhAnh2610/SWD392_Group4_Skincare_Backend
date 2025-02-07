using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.Enums;

namespace CleanArchitecture.Presentation.Endpoints
{
    public class RefundItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/refunditem").WithTags("Refund Item Management");

            group.MapGet("/", async (IRefundItemService refundItemService) =>
            {
                var result = await refundItemService.GetAllRefundItemsAsync();
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<List<RefundItemResponse>>.SuccessResponse(result.Data!, "Refund Items Retrieved Successfully."));
                }
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            })
            .WithName("GetRefundItems")
            .Produces<ApiResponse<List<RefundItemResponse>>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("GetRefundItems")
            .WithDescription("Get All Refund Items")
            .RequireAuthorization();

            group.MapGet("/{id}", async (IRefundItemService refundItemService, Guid id) =>
            {
                var result = await refundItemService.GetRefundItemByIdAsync(id);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<RefundItemResponse>.SuccessResponse(result.Data!, "Refund Item Retrieved Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<RefundItemResponse>.FailureResponse(result.Errors, "Refund Item Not Found.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("GetRefundItemById")
            .Produces<ApiResponse<RefundItemResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("GetRefundItemById")
            .WithDescription("Get Refund Item by ID")
            .RequireAuthorization();

            group.MapPost("/", async (IRefundItemService refundItemService, CreateRefundItemRequest request) =>
            {
                var result = await refundItemService.CreateRefundItemAsync(request);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<RefundItemResponse>.SuccessResponse(result.Data!, "Refund Item Created Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status400BadRequest => Results.BadRequest(ApiResponse<RefundItemResponse>.FailureResponse(result.Errors, "Invalid Input.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("CreateRefundItem")
            .Produces<ApiResponse<RefundItemResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("CreateRefundItem")
            .WithDescription("Create New Refund Item")
            .RequireAuthorization();

            group.MapPut("/", async (IRefundItemService refundItemService, UpdateRefundItemRequest request) =>
            {
                var result = await refundItemService.UpdateRefundItemAsync(request);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<RefundItemResponse>.SuccessResponse(result.Data!, "Refund Item Updated Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status400BadRequest => Results.BadRequest(ApiResponse<RefundItemResponse>.FailureResponse(result.Errors, "Invalid Input.")),
                    StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<RefundItemResponse>.FailureResponse(result.Errors, "Refund Item Not Found.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("UpdateRefundItem")
            .Produces<ApiResponse<RefundItemResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("UpdateRefundItem")
            .WithDescription("Update Refund Item")
            .RequireAuthorization();

            group.MapDelete("/{id}", async (IRefundItemService refundItemService, Guid id) =>
            {
                var result = await refundItemService.DeleteRefundItemAsync(id);
                if (result.IsSuccess)
                {
                    return Results.Ok(ApiResponse<string>.SuccessResponse(result.Data!, "Refund Item Deleted Successfully."));
                }
                return result.Status switch
                {
                    StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<string>.FailureResponse(result.Errors, "Refund Item Not Found.")),
                    _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                };
            })
            .WithName("DeleteRefundItem")
            .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("DeleteRefundItem")
            .WithDescription("Delete Refund Item")
            .RequireAuthorization();
        }
    }
}
