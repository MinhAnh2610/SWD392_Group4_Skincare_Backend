using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
  public void Configure(EntityTypeBuilder<Tag> builder)
  {
    builder.HasKey(tag => tag.Id);

    builder.HasMany(tag => tag.BlogTags)
      .WithOne(blogTags => blogTags.Tag)
      .HasForeignKey(blogTags => blogTags.TagId);
  }
}