using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
    public class CosmeticConfiguratio : IEntityTypeConfiguration<Cosmetic>
    {
        public void Configure(EntityTypeBuilder<Cosmetic> builder)
        {
            builder.HasKey(cosmetic => cosmetic.Id);
            
            builder.HasMany(cosmetic =>cosmetic.OrderItems)
                .WithOne(orderItem => orderItem.Cosmetic);

        }
    }
}
