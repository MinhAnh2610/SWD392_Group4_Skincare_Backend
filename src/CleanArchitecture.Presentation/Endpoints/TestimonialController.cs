using CleanArchitecture.Application.DTOs.TestimonialDto;

namespace CleanArchitecture.Presentation.Endpoints;

public class TestimonialController : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/testimonial").WithTags("Testimonial Management");

    #region Get Testimonials API
    group.MapGet("/", async (ITestimonialService service) =>
    {
      var result = await service.GetTestimonialsAsync();
      if (result != null)
      {
        return Results.Ok(ApiResponse<List<TestimonialResponse>>.SuccessResponse(result.Data!, "Retrieved Testimonials Successfully."));
      }

      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    })
    .WithName("GetTestimonials")
    .Produces<ApiResponse<List<TestimonialResponse>>>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("GetTestimonials")
    .WithDescription("Get Testimonials");
    #endregion
  }
}
