using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
{
  public TestimonialRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
