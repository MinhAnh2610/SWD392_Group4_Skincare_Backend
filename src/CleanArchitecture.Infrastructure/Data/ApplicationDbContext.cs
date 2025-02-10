using CleanArchitecture.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Data;

public interface IApplicationDbContext
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
       
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    builder.ApplyConfiguration(new BlogConfiguration());
        builder.ApplyConfiguration(new BlogTagConfiguration());
        builder.ApplyConfiguration(new CartItemConfiguration());
        builder.ApplyConfiguration(new CartConfiguration());
        builder.ApplyConfiguration(new FeedbackConfiguration());
        builder.ApplyConfiguration(new FeedbackConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new QuestionConfiguration());
        builder.ApplyConfiguration(new QuestionOptionConfiguration());
        builder.ApplyConfiguration(new QuestionTypeConfiguration());
        builder.ApplyConfiguration(new QuizConfiguration());
        builder.ApplyConfiguration(new RoutineConfiguration());
        builder.ApplyConfiguration(new RoutineStepConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new PaymentConfiguration());
        builder.ApplyConfiguration(new RefundConfiguration());
        builder.ApplyConfiguration(new RefundItemConfiguration());
        base.OnModelCreating(builder);
  }
}
