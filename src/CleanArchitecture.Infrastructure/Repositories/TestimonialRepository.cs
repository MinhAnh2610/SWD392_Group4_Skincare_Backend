using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
{
  public TestimonialRepository(ApplicationDbContext context) : base(context)
  {
    
  }

  public async Task<List<Testimonial>> GetAllTestimonialsAsync()
  {
    return await _context.Testimonials
      .Include(t => t.Customer)
      .ToListAsync();
  }
}
