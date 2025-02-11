using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Presentation.Endpoints;

public class PolicyController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/policy").WithTags("Company Policies");

    #region Get Company Policies API
    group.MapGet("/", async (IUnitOfWork unitOfWork) =>
    {
      var result = await unitOfWork.Policies.GetAllAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<Policy>>.SuccessResponse(result, "Retrieved Company Policies Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetPolicies")
    .Produces<ApiResponse<List<Policy>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetPolicies")
    .WithDescription("Get Policies");
    #endregion
  }
}
