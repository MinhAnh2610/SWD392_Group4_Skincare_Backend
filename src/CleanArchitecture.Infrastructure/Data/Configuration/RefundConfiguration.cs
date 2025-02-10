using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RefundConfiguration : IEntityTypeConfiguration<Refund>
{
    public void Configure(EntityTypeBuilder<Refund> builder)
    {
        builder.HasKey(refund => refund.Id);

        builder.HasOne(refund => refund.Order)
            .WithMany(order => order.Refunds)
            .HasForeignKey(refund => refund.OrderId)
            .IsRequired();

        builder.HasOne(refund => refund.Staff)
            .WithMany(staff => staff.Refunds)
            .HasForeignKey(refund => refund.StaffId)
            .IsRequired();
    }
}