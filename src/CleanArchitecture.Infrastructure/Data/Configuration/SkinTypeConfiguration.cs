using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
    public class SkinTypeConfiguration : IEntityTypeConfiguration<SkinType>
    {
        public void Configure(EntityTypeBuilder<SkinType> builder)
        {
            builder.HasKey(skinType => skinType.Id);

            builder.HasMany(skinType => skinType.Customers)
                .WithOne(customer => customer.SkinType);

            builder.HasMany(skinType => skinType.Cosmetics)
                .WithOne(cosmetic => cosmetic.SkinType);

            builder.HasMany(skinType => skinType.Routines)
                .WithOne(routine => routine.SkinType);
        }
    }
}
