using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(cart => cart.Id);

        builder.HasOne(cart => cart.Customer)
            .WithOne()
            .HasForeignKey<Cart>(cart => cart.Id)
            .IsRequired();
    }
}