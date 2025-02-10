using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.HasKey(routine => routine.Id);

        builder.HasMany(routine => routine.RoutineSteps)
            .WithOne(step => step.Routine);

        builder.HasOne(routine => routine.SkinType)
            .WithMany(type => type.Routines);
            // TODO: Continue
    }
}