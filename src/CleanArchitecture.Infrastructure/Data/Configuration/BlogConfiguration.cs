using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(blog => blog.Id);
        
        builder.HasOne(blog => blog.Staff)
            .WithMany(staff => staff.Blogs);
        
        builder.HasMany(blog => blog.BlogTags);
    }
}