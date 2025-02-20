using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(order => order.Id);

    builder.HasOne(order => order.Customer)
        .WithMany(customer => customer.Orders)
        .HasForeignKey(order => order.CustomerId);

    builder.HasOne(order => order.Coupon)
        .WithMany(coupon => coupon.Orders)
        .HasForeignKey(order => order.CouponId);

    builder.HasMany(order => order.OrderItems)
        .WithOne(orderItem => orderItem.Order)
        .HasForeignKey(orderItem => orderItem.OrderId);
  }
}