using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
{
  public void Configure(EntityTypeBuilder<QuestionType> builder)
  {
    builder.HasKey(questionType => questionType.Id);

    builder.HasMany(questionType => questionType.Questions)
      .WithOne(question => question.QuestionType)
      .HasForeignKey(question => question.QuestionTypeId);
  }
}