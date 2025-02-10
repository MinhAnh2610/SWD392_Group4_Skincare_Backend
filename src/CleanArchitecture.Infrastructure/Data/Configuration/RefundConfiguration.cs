using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data.Configuration
{
    public class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.HasKey(refund => refund.Id);
            builder.HasOne(refund => refund.Order)
                .WithMany(order => order.Refunds);
            builder.HasOne(refund => refund.Staff)
                .WithMany(staff => staff.Refunds);
            builder.HasMany(refund => refund.RefundItems);
        }
    }
}
