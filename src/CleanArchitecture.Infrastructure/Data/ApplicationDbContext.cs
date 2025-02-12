using CleanArchitecture.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
  
  public ApplicationDbContext()
  {
  }

  public DbSet<Batch> Batches => Set<Batch>();
  public DbSet<Blog> Blogs => Set<Blog>();
  public DbSet<BlogTag> BlogsTags => Set<BlogTag>(); 
  public DbSet<Brand> Brands => Set<Brand>();
  public DbSet<Cart> Carts => Set<Cart>();
  public DbSet<CartItem> CartItems => Set<CartItem>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<CompanyInformation> CompanyInformation => Set<CompanyInformation>();
  public DbSet<Cosmetic> Cosmetics => Set<Cosmetic>();
  public DbSet<CosmeticImage> CosmeticsImages => Set<CosmeticImage>();
  public DbSet<CosmeticSubCategory> CosmeticSubCategories => Set<CosmeticSubCategory>();
  public DbSet<CosmeticType> CosmeticTypes => Set<CosmeticType>();
  public DbSet<Coupon> Coupons => Set<Coupon>();
  public DbSet<FAQ> FAQs => Set<FAQ>();
  public DbSet<Feedback> Feedbacks => Set<Feedback>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();
  public DbSet<Payment> Payments => Set<Payment>();
  public DbSet<Policy> Policies => Set<Policy>();
  public DbSet<Question> Questions => Set<Question>();
  public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();
  public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
  public DbSet<Quiz> Quizzes => Set<Quiz>();
  public DbSet<Refund> Refunds => Set<Refund>();
  public DbSet<RefundItem> RefundItems => Set<RefundItem>();  
  public DbSet<Routine> Routines => Set<Routine>();
  public DbSet<RoutineStep> RoutineSteps => Set<RoutineStep>();
  public DbSet<SkinType> SkinTypes => Set<SkinType>();  
  public DbSet<SubCategory> SubCategories => Set<SubCategory>();
  public DbSet<Tag> Tags => Set<Tag>();
  public DbSet<Testimonial> Testimonials => Set<Testimonial>();

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
