using CleanArchitecture.Application.DTOs.FeedbackDto;
using IdentityModel;
using System.Security.Claims;

namespace CleanArchitecture.Presentation.Endpoints;

public class FeedbackController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/feedback").WithTags("Feedbacks Management");

    #region Get Feedbacks API
    group.MapGet("/", async (IFeedbackService service) =>
    {
      var result = await service.GetAllFeedbacksAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<FeedbackResponse>>.SuccessResponse(result.Data!, "Retrieved Feedbacks Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetFeedbacks")
    .Produces<ApiResponse<List<FeedbackResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetFeedbacks")
    .WithDescription("Get Feedbacks");
    #endregion

    #region Leave Feedback API
    group.MapPost("/", async (IFeedbackService service, IHttpContextAccessor httpContextAccessor, FeedbackRequest request) =>
    {
      var user = httpContextAccessor.HttpContext?.User;
      var userId = user!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
      var userName = user!.FindFirst(JwtClaimTypes.Name)!.Value;
      var result = await service.CreateFeedbackAsync(request, Guid.Parse(userId), userName);
      return result.Match(Message.SUCCESSFUL_CREATED("Feedback"));
    })
    .WithName("LeaveFeedback")
    .Produces<ApiResponse<List<FeedbackResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("LeaveFeedback")
    .WithDescription("Leave Feedback")
    .RequireAuthorization();
    #endregion

    #region Get Feedback By Customer API
    group.MapGet("customer/{id:guid}", async (Guid id, IFeedbackService feedbackService) =>
    {
      var result = await feedbackService.GetFeedbacksByCustomerIdAsync(id);
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<List<FeedbackResponse>>.SuccessResponse(
            result.Data!,
            "Retrieved customer feedback successfully."
        ));
      }
      return Results.StatusCode(result.Status);
    })
    .WithName("GetFeedbacksByCustomer")
    .Produces<ApiResponse<List<FeedbackResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetFeedbacksByCustomer")
    .WithDescription("Get Feedbacks By Customer")
    .RequireAuthorization();
    #endregion
  }
}
