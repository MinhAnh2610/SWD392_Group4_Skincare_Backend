namespace CleanArchitecture.Domain.RepositoryContracts;

public interface ITestimonialRepository : IGenericRepository<Testimonial>
{
  Task<List<Testimonial>> GetAllTestimonialsAsync();
}
