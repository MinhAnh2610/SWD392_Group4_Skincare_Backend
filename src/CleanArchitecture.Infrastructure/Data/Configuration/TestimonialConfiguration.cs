using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
{
    public void Configure(EntityTypeBuilder<Testimonial> builder)
    {
        builder.HasKey(testimonial => testimonial.Id);

        builder.HasOne(testimonial => testimonial.Customer)
            .WithMany(customer => customer.Testimonials);
    }
}