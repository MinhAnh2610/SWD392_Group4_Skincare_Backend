using CleanArchitecture.Application.DTOs.TestimonialDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface ITestimonialService
{
  Task<Result<List<TestimonialResponse>>> GetTestimonialsAsync();
}
