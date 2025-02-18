// File: Presentation/Endpoints/FeedbackController.cs
using CleanArchitecture.Application.DTOs.FeedbackDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class FeedbackController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("/feedbacks").WithTags("Feedback Management");

      // GET /feedbacks → Retrieve all feedback entries
      group.MapGet("/", async (IFeedbackService feedbackService) =>
      {
        var result = await feedbackService.GetAllFeedbacksAsync();
        if (result.IsSuccess)
        {
          return Results.Ok(ApiResponse<List<FeedbackResponse>>.SuccessResponse(
              result.Data!,
              "Retrieved feedbacks successfully."
          ));
        }
        return Results.StatusCode(result.Status);
      })
      .WithName("GetAllFeedbacks")
      .Produces<ApiResponse<List<FeedbackResponse>>>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .RequireAuthorization();
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
            .RequireAuthorization();
    }

  }
}
