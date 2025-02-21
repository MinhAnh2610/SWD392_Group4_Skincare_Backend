using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;
using CleanArchitecture.Application.DTOs.SkinTypeDto;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArchitecture.Presentation.Endpoints;

public class QuizController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/quiz").WithTags("Quiz Management");

    #region Get Quiz API
    group.MapGet("/", async (IQuizService service) =>
    {
      var result = await service.GetQuizAsync();
      if (result.IsSuccess)
      {
        return Results.Ok(ApiResponse<QuizResponse>.SuccessResponse(result.Data!, "Retrieved Quiz Successfully."));
      }

      return result.Status switch
      {
        StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<QuizResponse>.FailureResponse(result.Errors, "Resource Not Found")),
        _ => Results.StatusCode(StatusCodes.Status500InternalServerError),
      };
    })
    .WithName("GetQuiz")
    .Produces<ApiResponse<QuizResponse>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetQuiz")
    .WithDescription("Get Skin Type Quiz");
    #endregion

    #region Submit Quiz API
    group.MapPost("{id}/submit", async (
      IQuizService quizService,
      IQuizResultService quizResultService,
      IHttpContextAccessor httpContextAccessor,
      [FromRoute] Guid id,
      [FromBody] QuizSubmitRequest request) =>
    {
      var result = await quizService.ProcessQuizAsync(id, request);
      var user = httpContextAccessor.HttpContext?.User;

      if (result.IsSuccess)
      {
        if (user!.Identity!.IsAuthenticated)
        {
          var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
          await quizResultService.SaveQuizResultAsync(Guid.Parse(userId), id, request, result.Data!.First());
        }

        return Results.Ok(ApiResponse<List<RoutineResponse>>.SuccessResponse(result.Data!, "Submitted Quiz Successfully."));
      }

      return result.Status switch
      {
        StatusCodes.Status404NotFound => Results.NotFound(ApiResponse<List<RoutineResponse>>.FailureResponse(result.Errors, "Resource Not Found")),
        _ => Results.StatusCode(StatusCodes.Status500InternalServerError),
      };
    })
    .WithName("SubmitQuiz")
    .Produces<ApiResponse<List<RoutineResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("SubmitQuiz")
    .WithDescription("Submit Skin Type Quiz");
    #endregion
  }
}
