using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Presentation.Endpoints;

public class CompanyInformationController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/company-info").WithTags("Company Information Management");

    #region Get Company Information API
    group.MapGet("/", async (IUnitOfWork unitOfWork) =>
    {
      var result = await unitOfWork.CompanyInformation.GetAllAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<CompanyInformation>>.SuccessResponse(result, "Retrieved Company Information Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetCompanyInformation")
    .Produces<ApiResponse<List<CompanyInformation>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetCompanyInformation")
    .WithDescription("Get Company Information");
    #endregion
  }
}
