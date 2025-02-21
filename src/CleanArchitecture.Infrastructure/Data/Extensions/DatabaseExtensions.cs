using CleanArchitecture.Application.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
  public static async Task InitializeDatabaseAsync(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    //context.Database.MigrateAsync().GetAwaiter().GetResult();

    await SeedAsync(context, roleManager, userManager);
  }

  private static async Task SeedAsync(ApplicationDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
  {
    await SeedRoleAsync(context, roleManager);
    await SeedUserAsync(context, userManager);
    await SeedBatchAsync(context);
    await SeedBlogAsync(context);
    await SeedBlogTagAsync(context);
    await SeedBrandAsync(context);
    await SeedCartAsync(context);
    await SeedCartItemAsync(context);
    await SeedCategoryAsync(context);
    await SeedCompanyInformationAsync(context);
    await SeedCosmeticAsync(context);
    await SeedCosmeticImageAsync(context);
    await SeedCosmeticSubCategoryAsync(context);
    await SeedCosmeticTypeAsync(context);
    await SeedCouponAsync(context);
    await SeedFAQAsync(context);
    await SeedFeedbackAsync(context);
    await SeedOrderAsync(context);
    await SeedOrderItemAsync(context);
    await SeedPolicyAsync(context);
    await SeedQuestionTypeAsync(context);
    await SeedQuizAsync(context);
    await SeedRoutineAsync(context);
    await SeedRoutineStepAsync(context);
    await SeedSkinTypeAsync(context);
    await SeedSubCategoryAsync(context);
    await SeedTagAsync(context);
    await SeedTestimonialAsync(context);
    await context.SaveChangesAsync();
  }

  private static async Task SeedRoleAsync(ApplicationDbContext context, RoleManager<Role> roleManager)
  {
    if (!await context.Roles.AnyAsync())
    {
      foreach (var role in InitialData.Roles)
      {
        await roleManager.CreateAsync(role);
      }
    }
  }

  private static async Task SeedUserAsync(ApplicationDbContext context, UserManager<User> userManager)
  {
    if (!await context.Users.AnyAsync())
    {
      var managers = InitialData.Users.Skip(0).Take(2).ToList();
      var staffs = InitialData.Users.Skip(2).Take(3).ToList();
      var customers = InitialData.Users.Skip(5).Take(2).ToList();

      foreach (var user in managers)
      {
        await userManager.CreateAsync(user, "12345");
        await userManager.AddToRoleAsync(user, Roles.Manager);
      }

      foreach (var user in staffs)
      {
        await userManager.CreateAsync(user, "12345");
        await userManager.AddToRoleAsync(user, Roles.Staff);
      }

      foreach (var user in customers)
      {
        await userManager.CreateAsync(user, "12345");
        await userManager.AddToRoleAsync(user, Roles.Customer);
      }
    }
  }

  private static async Task SeedBatchAsync(ApplicationDbContext context)
  {
    if (!await context.Batches.AnyAsync())
    {
      await context.Batches.AddRangeAsync(InitialData.Batches);
    }
  }

  private static async Task SeedBlogAsync(ApplicationDbContext context)
  {
    if (!await context.Blogs.AnyAsync())
    {
      await context.Blogs.AddRangeAsync(InitialData.Blogs);
    }
  }

  private static async Task SeedBlogTagAsync(ApplicationDbContext context)
  {
    if (!await context.BlogsTags.AnyAsync())
    {
      await context.BlogsTags.AddRangeAsync(InitialData.BlogTags);
    }
  }

  private static async Task SeedBrandAsync(ApplicationDbContext context)
  {
    if (!await context.Brands.AnyAsync())
    {
      await context.Brands.AddRangeAsync(InitialData.Brands);
    }
  }

  private static async Task SeedCartAsync(ApplicationDbContext context)
  {
    if (!await context.Carts.AnyAsync())
    {
      await context.Carts.AddRangeAsync(InitialData.Carts);
    }
  }

  private static async Task SeedCartItemAsync(ApplicationDbContext context)
  {
    if (!await context.CartItems.AnyAsync())
    {
      await context.CartItems.AddRangeAsync(InitialData.CartItems);
    }
  }

  private static async Task SeedCategoryAsync(ApplicationDbContext context)
  {
    if (!await context.Categories.AnyAsync())
    {
      await context.Categories.AddRangeAsync(InitialData.Categories);
    }
  }

  private static async Task SeedCompanyInformationAsync(ApplicationDbContext context)
  {
    if (!await context.CompanyInformation.AnyAsync())
    {
      await context.CompanyInformation.AddRangeAsync(InitialData.CompanyInfos);
    }
  }

  private static async Task SeedCosmeticAsync(ApplicationDbContext context)
  {
    if (!await context.Cosmetics.AnyAsync())
    {
      await context.Cosmetics.AddRangeAsync(InitialData.Cosmetics);
    }
  }

  private static async Task SeedCosmeticImageAsync(ApplicationDbContext context)
  {
    if (!await context.CosmeticsImages.AnyAsync())
    {
      await context.CosmeticsImages.AddRangeAsync(InitialData.CosmeticImages);
    }
  }

  private static async Task SeedCosmeticSubCategoryAsync(ApplicationDbContext context)
  {
    if (!await context.CosmeticSubCategories.AnyAsync())
    {
      await context.CosmeticSubCategories.AddRangeAsync(InitialData.CosmeticSubCategories);
    }
  }

  private static async Task SeedCosmeticTypeAsync(ApplicationDbContext context)
  {
    if (!await context.CosmeticTypes.AnyAsync())
    {
      await context.CosmeticTypes.AddRangeAsync(InitialData.CosmeticTypes);
    }
  }

  private static async Task SeedCouponAsync(ApplicationDbContext context)
  {
    if (!await context.Coupons.AnyAsync())
    {
      await context.Coupons.AddRangeAsync(InitialData.Coupons);
    }
  }

  private static async Task SeedFAQAsync(ApplicationDbContext context)
  {
    if (!await context.FAQs.AnyAsync())
    {
      await context.FAQs.AddRangeAsync(InitialData.FAQs);
    }
  }

  private static async Task SeedFeedbackAsync(ApplicationDbContext context)
  {
    if (!await context.Feedbacks.AnyAsync())
    {
      await context.Feedbacks.AddRangeAsync(InitialData.Feedbacks);
    }
  }

  private static async Task SeedOrderAsync(ApplicationDbContext context)
  {
    if (!await context.Orders.AnyAsync())
    {
      await context.Orders.AddRangeAsync(InitialData.Orders);
    }
  }

  private static async Task SeedOrderItemAsync(ApplicationDbContext context)
  {
    if (!await context.OrderItems.AnyAsync())
    {
      await context.OrderItems.AddRangeAsync(InitialData.OrderItems);
    }
  }

  private static async Task SeedPolicyAsync(ApplicationDbContext context)
  {
    if (!await context.Policies.AnyAsync())
    {
      await context.Policies.AddRangeAsync(InitialData.Policies);
    }
  }

  private static async Task SeedQuestionTypeAsync(ApplicationDbContext context)
  {
    if (!await context.QuestionTypes.AnyAsync())
    {
      await context.QuestionTypes.AddRangeAsync(InitialData.QuestionsTypes);
    }
  }

  private static async Task SeedQuizAsync(ApplicationDbContext context)
  {
    if (!await context.Quizzes.AnyAsync())
    {
      await context.Quizzes.AddRangeAsync(InitialData.Quiz);
    }
  }

  private static async Task SeedRoutineAsync(ApplicationDbContext context)
  {
    if (!await context.Routines.AnyAsync())
    {
      await context.Routines.AddRangeAsync(InitialData.Routines);
    }
  }

  private static async Task SeedRoutineStepAsync(ApplicationDbContext context)
  {
    if (!await context.RoutineSteps.AnyAsync())
    {
      await context.RoutineSteps.AddRangeAsync(InitialData.RoutineSteps);
    }
  }

  private static async Task SeedSkinTypeAsync(ApplicationDbContext context)
  {
    if (!await context.SkinTypes.AnyAsync())
    {
      await context.SkinTypes.AddRangeAsync(InitialData.SkinTypes);
    }
  }

  private static async Task SeedSubCategoryAsync(ApplicationDbContext context)
  {
    if (!await context.SubCategories.AnyAsync())
    {
      await context.SubCategories.AddRangeAsync(InitialData.SubCategories);
    }
  }

  private static async Task SeedTagAsync(ApplicationDbContext context)
  {
    if (!await context.Tags.AnyAsync())
    {
      await context.Tags.AddRangeAsync(InitialData.Tags);
    }
  }

  private static async Task SeedTestimonialAsync(ApplicationDbContext context)
  {
    if (!await context.Testimonials.AnyAsync())
    {
      await context.Testimonials.AddRangeAsync(InitialData.Testimonials);
    }
  }
}
