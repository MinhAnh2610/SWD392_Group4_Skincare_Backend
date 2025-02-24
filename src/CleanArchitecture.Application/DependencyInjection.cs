using CleanArchitecture.Application.DTOs.Auth;
using CleanArchitecture.Application.DTOs.CouponDTO;
using CleanArchitecture.Application.DTOs.QuestionDto;
using CleanArchitecture.Application.DTOs.ReportDto;
using CleanArchitecture.Application.DTOs.RoleDto;
using CleanArchitecture.Application.DTOs.UserDto;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Strategies;
using CleanArchitecture.Application.Strategies.BlogFilterStrategy;
using CleanArchitecture.Application.Validators;
using CleanArchitecture.Application.Validators.Auth;
using CleanArchitecture.Application.Validators.Quiz;
using CleanArchitecture.Application.Validators.Blog;
using CleanArchitecture.Application.Validators.Role;
using CleanArchitecture.Application.Validators.User;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using QuestPDF.Infrastructure;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices
    (this IServiceCollection services, IConfiguration configuration)
  {

    QuestPDF.Settings.License = LicenseType.Community;
    
    // PdfReportGenerateStrategy strategy = new PdfReportGenerateStrategy();
    // strategy.Generate(new GenerateReportRequest("pdf", "month", DateTime.Now, DateTime.Today));
    
    services.AddFeatureManagement();
    services.AddHttpContextAccessor();

    // Add all validators
    services.AddValidatorsFromAssemblyContaining<CreateBlogRequestValidator>();

    #region Coupon Validatorss

    services.AddScoped<IValidator<QuestionAddRequest>, QuestionAddRequestValidator>();
    services.AddScoped<IValidator<QuestionUpdateRequest>, QuestionUpdateRequestValidator>();

    #endregion

    // Add identity server 4 validator for owner password
    services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

    // Add services
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IBatchService, BatchService>();
    services.AddScoped<IBlogService, BlogService>();
    services.AddScoped<IBrandService, BrandService>();
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<ICosmeticService, CosmeticService>();
    services.AddScoped<ICosmeticImageService, CosmeticImageService>();
    services.AddScoped<ICosmeticTypeService, CosmeticTypeService>();
    services.AddScoped<ICouponService, CouponService>();
    services.AddScoped<IFeedbackService, FeedbackService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IPaymentService, PaymentService>();
    services.AddScoped<IQuizService, QuizService>();
    services.AddScoped<IQuizResultService, QuizResultService>();
    services.AddScoped<IRefundService, RefundService>();
    services.AddScoped<IRefundItemService, RefundItemService>();
    services.AddScoped<ISubCategoryService, SubCategoryService>();
    services.AddScoped<ISkinTypeService, SkinTypeService>();
    services.AddScoped<ITestimonialService, TestimonialService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IRoutineService, RoutineService>();
    services.AddScoped<ITimeZoneService, TimeZoneService>();
    services.AddScoped<IVnPayIntegrationService, VnPayIntegrationService>();
    services.AddScoped<IErrorFactory, ErrorFactory>();
    services.AddScoped<IReportService, ReportService>();

    #region Add Strategies

    services.AddSingleton<IBlogFilterStrategy, ContentFilterStrategy>();
    services.AddSingleton<IBlogFilterStrategy, TitleFilterStrategy>();
    services.AddSingleton<IBlogFilterStrategy, StaffUsernameFilterStrategy>();
    services.AddSingleton<IBlogFilterStrategy, SortOrderFilterStrategy>();

    services.AddSingleton<IReportGenerateStrategy, PdfReportGenerateStrategy>();
    services.AddSingleton<IReportGenerateStrategy, WordReportGenerateStrategy>();

    #endregion


    return services;
  }
}