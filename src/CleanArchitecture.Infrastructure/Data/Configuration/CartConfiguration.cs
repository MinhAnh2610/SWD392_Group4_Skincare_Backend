using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
  public void Configure(EntityTypeBuilder<Cart> builder)
  {
    builder.HasKey(cart => cart.Id);

    builder.HasOne(cart => cart.Customer);

    builder.HasMany(cart => cart.CartItems)
      .WithOne(cartItems => cartItems.Cart)
      .HasForeignKey(cartItems => cartItems.CartId);
  }
}