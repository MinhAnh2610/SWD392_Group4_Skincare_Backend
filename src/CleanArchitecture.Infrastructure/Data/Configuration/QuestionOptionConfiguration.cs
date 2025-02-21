using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
  public void Configure(EntityTypeBuilder<QuestionOption> builder)
  {
    builder.HasKey(questionOption => questionOption.Id);

    builder.HasOne(questionOption => questionOption.Question)
      .WithMany(question => question.QuestionOptions)
      .HasForeignKey(questionOption => questionOption.QuestionId);


    builder.HasOne(questionOption => questionOption.QuestionType)
        .WithMany(questionType => questionType.QuestionOptions)
        .HasForeignKey(questionOption => questionOption.QuestionTypeId);
  }
}