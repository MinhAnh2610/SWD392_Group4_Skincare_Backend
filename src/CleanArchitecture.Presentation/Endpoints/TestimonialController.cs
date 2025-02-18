
using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Presentation.Endpoints;

public class TestimonialController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/testimonial").WithTags("Testimonial Management");

    #region Get Testimonials API
    group.MapGet("/", async (IUnitOfWork unitOfWork) =>
    {
      var result = await unitOfWork.Testimonials.GetAllAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<Testimonial>>.SuccessResponse(result, "Retrieved Testimonials Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetTestimonials")
    .Produces<ApiResponse<CompanyInformation>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetTestimonials")
    .WithDescription("Get Testimonials");
    #endregion
  }
}
