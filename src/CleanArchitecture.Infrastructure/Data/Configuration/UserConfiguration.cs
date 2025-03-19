using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Tls;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(user => user.Id);

    builder.HasMany(user => user.Feedbacks)
      .WithOne(feedback => feedback.Customer)
      .HasForeignKey(feedback => feedback.CustomerId);

    builder.HasOne(user => user.SkinType)
      .WithMany(skinType => skinType.Customers)
      .HasForeignKey(user => user.SkinTypeId);

    builder.HasMany(user => user.Blogs)
      .WithOne(blog => blog.Staff)
      .HasForeignKey(blog => blog.StaffId);

    builder.HasMany(user => user.Refunds)
            .WithOne(refund => refund.Customer)
            .HasForeignKey(refund => refund.CustomerId);

    builder.HasMany(user => user.Orders)
        .WithOne(order => order.Customer)
        .HasForeignKey(order => order.CustomerId);

    builder.HasMany(user => user.Testimonials)
        .WithOne(testimonial => testimonial.Customer)
        .HasForeignKey(t => t.CustomerId);
    
  }
}