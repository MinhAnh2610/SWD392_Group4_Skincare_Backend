using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class RoutineStepConfiguration : IEntityTypeConfiguration<RoutineStep>
{
  public void Configure(EntityTypeBuilder<RoutineStep> builder)
  {
    builder.HasKey(routineStep => routineStep.Id);

    builder.HasOne(routineStep => routineStep.Routine)
        .WithMany(routine => routine.RoutineSteps)
        .HasForeignKey(routine => routine.RoutineId);

    builder.HasOne(routineStep => routineStep.Cosmetic)
        .WithMany(cosmetic => cosmetic.RoutineSteps)
        .HasForeignKey(routineStep => routineStep.CosmeticId);
  }
}