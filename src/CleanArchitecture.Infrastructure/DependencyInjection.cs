using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Domain.RepositoryContracts.Base;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;
using CleanArchitecture.Infrastructure.Auth;
using CleanArchitecture.Infrastructure.Data.Interceptors;
using CleanArchitecture.Infrastructure.Redis;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    // Add services to the container
    services.AddIdentityServer()
      //.AddAspNetIdentity<User>()
      .AddInMemoryApiScopes(Config.ApiScopes) // Define API scopes
      .AddInMemoryApiResources(Config.ApiResources) // Define API resources
      .AddInMemoryClients(Config.Clients) // Define clients
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddDeveloperSigningCredential() // Use for dev, use a real certificate in prod
      .AddProfileService<ProfileService>();

    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

    services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    {
      options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());

      string? connectionString = Environment.GetEnvironmentVariable("databaseConnectionString");
      // if (string.IsNullOrEmpty(connectionString))
      //   connectionString = configuration.GetConnectionString("DevDatabase");
      options.UseInMemoryDatabase("database");
      //options.UseNpgsql(connectionString);
    });

    services.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = configuration.GetConnectionString("Redis");
    });

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

    // Register Unit of Work
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Register Generic Repository
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    #region Register Repositories

    services.AddScoped(typeof(IBatchRepository), typeof(BatchRepository));
    services.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
    services.AddScoped(typeof(IBlogTagRepository), typeof(BlogTagRepository));
    services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
    services.AddScoped(typeof(ICartItemRepository), typeof(CartItemRepository));
    services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
    services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
    services.AddScoped(typeof(ICompanyInformationRepository), typeof(CompanyInformationRepository));
    services.AddScoped(typeof(ICosmeticImageRepository), typeof(CosmeticImageRepository));
    services.AddScoped(typeof(ICosmeticRepository), typeof(CosmeticRepository));
    services.AddScoped(typeof(ICosmeticSubCategoryRepository),
      typeof(CosmeticSubCategoryRepository));
    services.AddScoped(typeof(ICosmeticTypeRepository), typeof(CosmeticTypeRepository));
    services.AddScoped(typeof(ICouponRepository), typeof(CouponRepository));
    services.AddScoped(typeof(IFAQRepository), typeof(FAQRepository));
    services.AddScoped(typeof(IFeedbackRepository), typeof(FeedbackRepository));
    services.AddScoped(typeof(IOrderItemRepository), typeof(OrderItemRepository));
    services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
    services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
    services.AddScoped(typeof(IPolicyRepository), typeof(PolicyRepository));
    services.AddScoped(typeof(IQuestionOptionRepository), typeof(QuestionOptionRepository));
    services.AddScoped(typeof(IQuestionRepository), typeof(QuestionRepository));
    services.AddScoped(typeof(IQuestionTypeRepository), typeof(QuestionTypeRepository));
    services.AddScoped(typeof(IQuizRepository), typeof(QuizRepository));
    services.AddScoped(typeof(IRefundRepository), typeof(RefundRepository));
    services.AddScoped(typeof(IRefundItemRepository), typeof(RefundItemRepository));
    services.AddScoped(typeof(IRoutineRepository), typeof(RoutineRepository));
    services.AddScoped(typeof(IRoutineStepRepository), typeof(RoutineStepRepository));
    services.AddScoped(typeof(ISkinTypeRepository), typeof(SkinTypeRepository));
    services.AddScoped(typeof(ISubCategoryRepository), typeof(SubCategoryRepository));
    services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
    services.AddScoped(typeof(ITestimonialRepository), typeof(TestimonialRepository));

    #endregion

    // Register Redis Caching
    services.AddScoped<IRedisCacheRepository, RedisCacheRepository>();

    return services;
  }
}