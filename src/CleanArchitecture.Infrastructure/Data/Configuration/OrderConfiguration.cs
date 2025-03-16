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

    builder.Property(order => order.BillingAddress).IsRequired(false);
    builder.Property(order => order.TrackingNumber).IsRequired(false);
    builder.Property(order => order.DeliveryDate).IsRequired(false);
    builder.Property(order => order.ShippingAddress).IsRequired(false);
      

    builder.Property(order => order.OrderDate)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("CURRENT_TIMESTAMP");
    
    //builder.Property(order => order.DeliveryDate)
    //  .HasColumnType("timestamp")
    //  .HasDefaultValueSql("CURRENT_TIMESTAMP");
    
    builder.Property(order => order.CreateAt)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("CURRENT_TIMESTAMP");
    
    builder.Property(order => order.LastModified)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("CURRENT_TIMESTAMP");
  }
}