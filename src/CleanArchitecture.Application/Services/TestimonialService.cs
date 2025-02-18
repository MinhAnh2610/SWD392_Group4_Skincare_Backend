using CleanArchitecture.Application.DTOs.TestimonialDto;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class TestimonialService : ITestimonialService
{
  private readonly IUnitOfWork _unitOfWork;
  public TestimonialService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<List<TestimonialResponse>>> GetTestimonialsAsync()
  {
    var result = await _unitOfWork.Testimonials.GetAllTestimonialsAsync();

    return Result<List<TestimonialResponse>>.Success(result.Select(t => new TestimonialResponse
    {
      Id = t.Id,
      Content = t.Content,
      Rating = t.Rating,
      CustomerId = t.CustomerId,
      CustomerName = t.Customer.UserName!
    }).ToList(), StatusCodes.Status200OK);
  }
}
