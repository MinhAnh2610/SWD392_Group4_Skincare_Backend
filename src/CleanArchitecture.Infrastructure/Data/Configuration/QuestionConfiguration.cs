using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
  public void Configure(EntityTypeBuilder<Question> builder)
  {
    builder.HasKey(question => question.Id);

    builder.HasOne(question => question.Quiz)
      .WithMany(question => question.Questions)
      .HasForeignKey(question => question.QuizId);

    builder.HasMany(question => question.QuestionOptions)
      .WithOne(questionOption => questionOption.Question)
      .HasForeignKey(questionOption => questionOption.QuestionId);

    builder.HasOne(question => question.QuestionType)
        .WithMany(questionType => questionType.Questions)
        .HasForeignKey(question => question.QuestionTypeId);
  }
}