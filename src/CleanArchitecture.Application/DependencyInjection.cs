using CleanArchitecture.Application.DTOs.Auth;
using CleanArchitecture.Application.DTOs.CouponDTO;
using CleanArchitecture.Application.DTOs.RoleDto;
using CleanArchitecture.Application.DTOs.UserDto;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Validators;
using CleanArchitecture.Application.Validators.Auth;
using CleanArchitecture.Application.Validators.Role;
using CleanArchitecture.Application.Validators.User;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices
    (this IServiceCollection services, IConfiguration configuration)
  {

    services.AddFeatureManagement();
    services.AddHttpContextAccessor();

    // Add validators
    #region Auth Validators
    services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
    services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
    services.AddScoped<IValidator<ForgotPasswordRequest>, ForgotPasswordValidator>();
    services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordValidator>();
    services.AddScoped<IValidator<RefreshTokenRequest>, RefreshTokenValidator>();
    #endregion

    #region User Validators
    services.AddScoped<IValidator<UpdateProfileRequest>, UpdateProfileValidator>();
    services.AddScoped<IValidator<UserRequest>, UserValidator>();
    #endregion

    #region Role Validators
    services.AddScoped<IValidator<AssignRoleRequest>, AssignRoleValidator>();
    #endregion

    #region Coupon Validatorss
    services.AddScoped<IValidator<ApplyCouponRequest>, ApplyCouponRequestValidator>();
    #endregion

    // Add identity server 4 validator for owner password
    services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

    // Add services
    services.AddScoped<IAuthService, AuthService>();
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
    services.AddScoped<IRefundService, RefundService>();
    services.AddScoped<IRefundItemService, RefundItemService>();
    services.AddScoped<ISubCategoryService, SubCategoryService>();
    services.AddScoped<ISkinTypeService, SkinTypeService>();
    services.AddScoped<ITestimonialService, TestimonialService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IRoutineService, RoutineService>();
    return services;
  }
}
