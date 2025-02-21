using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
  public void Configure(EntityTypeBuilder<Blog> builder)
  {
    builder.HasKey(blog => blog.Id);

    builder.HasOne(blog => blog.Staff)
      .WithMany(staff => staff.Blogs)
      .HasForeignKey(blog => blog.StaffId);

    builder.HasMany(blog => blog.BlogTags)
      .WithOne(blogTags => blogTags.Blog)
      .HasForeignKey(blogTags => blogTags.BlogId);  
  }
}