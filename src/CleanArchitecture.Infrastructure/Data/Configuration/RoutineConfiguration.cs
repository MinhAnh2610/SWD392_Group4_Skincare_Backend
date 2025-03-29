using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
{
  public void Configure(EntityTypeBuilder<Routine> builder)
  {
    builder.HasKey(routine => routine.Id);

    builder.HasMany(routine => routine.RoutineSteps)
        .WithOne(routineStep => routineStep.Routine)
        .HasForeignKey(routineStep => routineStep.RoutineId);

    builder.HasOne(routine => routine.SkinType)
        .WithMany(skinType => skinType.Routines)
        .HasForeignKey(routine => routine.SkinTypeId);
  }
}