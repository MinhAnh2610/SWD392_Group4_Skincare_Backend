using CleanArchitecture.Application.DTOs.FeedbackDto;

namespace CleanArchitecture.Presentation.Endpoints;

public class FeedbackController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/feedback").WithTags("Feedbacks Management");

    #region Get Feedbacks API
    group.MapGet("/", async (IFeedbackService service) =>
    {
      var result = await service.GetFeedbacksAsync();
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
  }
}
