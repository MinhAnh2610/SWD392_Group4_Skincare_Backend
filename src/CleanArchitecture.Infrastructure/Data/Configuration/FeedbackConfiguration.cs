using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(feedback => feedback.Cosmetic)
            .WithMany(cosmetic => cosmetic.Feedbacks);

        builder.HasOne(feedback => feedback.Customer)
            .WithMany(customer => customer.Feedbacks);
    }
}