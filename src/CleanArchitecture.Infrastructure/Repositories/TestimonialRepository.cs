using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
{
  public TestimonialRepository(ApplicationDbContext context) : base(context)
  {
    
  }

  public override async Task<List<Testimonial>> GetAllAsync()
  {
    return await _context.Testimonials
      .Include(t => t.Customer)
      .ToListAsync();
  }
}
