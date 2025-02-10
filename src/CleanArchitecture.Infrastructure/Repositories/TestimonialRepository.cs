namespace CleanArchitecture.Infrastructure.Repositories;

public class TestimonialRepository : GenericRepository<Testimonial>
{
  public TestimonialRepository(ApplicationDbContext context) : base(context)
  {
    
  }
}
