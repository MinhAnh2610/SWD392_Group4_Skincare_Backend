using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Presentation.Endpoints;

public class FAQController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/faq").WithTags("Company FAQs");

    #region Get Company FAQs API
    group.MapGet("/", async (IUnitOfWork unitOfWork) =>
    {
      var result = await unitOfWork.FAQs.GetAllAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<FAQ>>.SuccessResponse(result, "Retrieved Company FAQs Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetFAQs")
    .Produces<ApiResponse<List<FAQ>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetFAQs")
    .WithDescription("Get FAQs");
    #endregion
  }
}
