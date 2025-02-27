using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
  public void Configure(EntityTypeBuilder<QuizResult> builder)
  {
    builder.HasOne(qr => qr.Customer)
            .WithMany()
            .HasForeignKey(qr => qr.CustomerId);

    builder.HasOne(qr => qr.Quiz)
        .WithMany()
        .HasForeignKey(qr => qr.QuizId);

    builder.HasOne(qr => qr.SkinType)
        .WithMany()
        .HasForeignKey(qr => qr.SkinTypeId);
  }
}
