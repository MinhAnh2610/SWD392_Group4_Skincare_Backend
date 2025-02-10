using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.HasKey(batch => batch.Id);
            builder.HasOne(batch => batch.Cosmetic)
                .WithMany(product => product.Batches);
        }
    }
}
