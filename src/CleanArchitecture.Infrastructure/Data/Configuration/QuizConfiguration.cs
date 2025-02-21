using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
  public void Configure(EntityTypeBuilder<Quiz> builder)
  {
    builder.HasKey(quiz => quiz.Id);

    builder.HasMany(quiz => quiz.Questions)
        .WithOne(question => question.Quiz)
        .HasForeignKey(question => question.QuizId);
  }
}