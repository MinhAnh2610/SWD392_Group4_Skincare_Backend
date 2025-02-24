using CleanArchitecture.Application.Services;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
{
  public void Configure(EntityTypeBuilder<QuizAnswer> builder)
  {
    builder.HasOne(q => q.QuizResult)
            .WithMany(qr => qr.QuizAnswers)
            .HasForeignKey(q => q.QuizResultId);

    builder.HasOne(q => q.Question)
        .WithMany()
        .HasForeignKey(q => q.QuestionId);
  }
}
