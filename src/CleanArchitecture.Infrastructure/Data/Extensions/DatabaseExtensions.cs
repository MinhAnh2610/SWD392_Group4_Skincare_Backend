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

  private static async Task SeedAsync(ApplicationDbContext context, RoleManager<Role> roleManager,
    UserManager<User> userManager)
  {
    await SeedRoleAsync(context, roleManager);
    await SeedUserAsync(context, userManager);
    await SeedSkinTypeAsync(context);
    await SeedBrandAsync(context);
    await SeedCosmeticTypeAsync(context);
    await SeedCosmeticAsync(context);
    await SeedEventsAsync(context);
    await SeedCosmeticPricesAsync(context);
    await SeedBatchAsync(context);
    await SeedTagAsync(context);
    await SeedBlogAsync(context);
    await SeedBlogTagAsync(context);

    await SeedCartAsync(context);
    await SeedCartItemAsync(context);
    await SeedCategoryAsync(context);
    await SeedCompanyInformationAsync(context);
    await SeedSubCategoryAsync(context);
    await SeedCosmeticImageAsync(context);
    await SeedCosmeticSubCategoryAsync(context);

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

  public static async Task SeedBatchAsync(ApplicationDbContext context)
  {
    if (!await context.Batches.AnyAsync())
    {
      // Retrieve the persisted cosmetics from the database
      var cosmetics = await context.Cosmetics.ToListAsync();

      var batches = cosmetics.Select(c => new Batch
      {
        Id = Guid.NewGuid(),
        CosmeticId = c.Id,
        Quantity = 200,
        ManufactureDate = new DateOnly(2025, 3, 15),
        ExportedDate = new DateOnly(2025, 4, 1),
        ExpirationDate = new DateOnly(2027, 3, 15)
      }).ToList();

      await context.Batches.AddRangeAsync(batches);
      await context.SaveChangesAsync();
    }
  }

  private static async Task SeedBlogAsync(ApplicationDbContext context)
  {
    if (!await context.Blogs.AnyAsync())
    {
      await context.Blogs.AddRangeAsync(InitialData.Blogs);
      await context.SaveChangesAsync();
    }
  }

  public static async Task SeedBlogTagAsync(ApplicationDbContext context)
  {
    if (!await context.BlogsTags.AnyAsync())
    {
      // Retrieve existing blogs and tags from the database
      var blogs = await context.Blogs.ToListAsync();
      var tags = await context.Tags.ToListAsync();

      // Ensure that there are at least two blogs and two tags in the database
      if (blogs.Count < 2 || tags.Count < 2)
      {
        throw new Exception("Not enough blogs or tags have been seeded.");
      }

      var blogTags = new List<BlogTag>
      {
        new BlogTag { BlogId = blogs[0].Id, TagId = tags[0].Id }, // AI & Tech
        new BlogTag { BlogId = blogs[1].Id, TagId = tags[1].Id } // Business  
      };

      await context.BlogsTags.AddRangeAsync(InitialData.BlogTags);
      await context.SaveChangesAsync();
    }
  }


  private static async Task SeedBrandAsync(ApplicationDbContext context)
  {
    if (!await context.Brands.AnyAsync())
    {
      await context.Brands.AddRangeAsync(InitialData.Brands);
      await context.SaveChangesAsync();
    }
  }

  public static async Task SeedCartAsync(ApplicationDbContext context)
  {
    if (!await context.Carts.AnyAsync())
    {
      // Retrieve existing customers from the database
      var customers = await context.Users.ToListAsync();

      // Ensure that there are enough customers
      if (customers.Count < 6)
      {
        throw new Exception("Not enough customers have been seeded.");
      }

      var carts = new List<Cart>
      {
        new Cart
        {
          Id = new Guid("A5D8471E-7C24-48D9-8233-CD598E6DD1C3"), CustomerId = customers[5].Id, TotalPrice = 89.97m
        }
      };

      await context.Carts.AddRangeAsync(carts);
      await context.SaveChangesAsync();
    }
  }

  public static async Task SeedCartItemAsync(ApplicationDbContext context)
  {
    if (!await context.CartItems.AnyAsync())
    {
      // Retrieve existing carts and cosmetics from the database
      var carts = await context.Carts.ToListAsync();
      var cosmetics = await context.Cosmetics.ToListAsync();

      // Ensure that there is at least one cart and two cosmetics
      if (carts.Count == 0 || cosmetics.Count < 2)
      {
        throw new Exception("Not enough carts or cosmetics have been seeded.");
      }

      var cartItems = new List<CartItem>
      {
        new CartItem { CartId = carts[0].Id, CosmeticId = cosmetics[0].Id, Quantity = 2 },
        new CartItem { CartId = carts[0].Id, CosmeticId = cosmetics[1].Id, Quantity = 1 }
      };

      await context.CartItems.AddRangeAsync(cartItems);
      await context.SaveChangesAsync();
    }
  }


  private static async Task SeedCategoryAsync(ApplicationDbContext context)
  {
    if (!await context.Categories.AnyAsync())
    {
      await context.Categories.AddRangeAsync(InitialData.Categories);
      await context.SaveChangesAsync();
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
      await context.SaveChangesAsync();
    }
  }

  private static async Task SeedCosmeticPricesAsync(ApplicationDbContext context)
  {
    if (!await context.CosmeticPrices.AnyAsync())
    {
      await context.CosmeticPrices.AddRangeAsync(InitialData.CosmeticPrices);
      await context.SaveChangesAsync();
    }
  }

  private static async Task SeedEventsAsync(ApplicationDbContext context)
  {
    if (!await context.Events.AnyAsync())
    {
      await context.Events.AddRangeAsync(InitialData.Events);
      await context.SaveChangesAsync();
    }
  }

  public static async Task SeedCosmeticImageAsync(ApplicationDbContext context)
  {
    if (!await context.CosmeticsImages.AnyAsync())
    {
      // Retrieve the cosmetics that have already been saved to the database.
      var cosmetics = await context.Cosmetics.ToListAsync();

      // Ensure that you have at least two cosmetics, as referenced below.
      if (cosmetics.Count < 2)
      {
        throw new Exception("Not enough cosmetics have been seeded to create cosmetic images.");
      }

      var cosmeticImages = new List<CosmeticImage>
      {
        new CosmeticImage
        {
          Id = new Guid("74D96977-165F-428B-8DE8-5488D6427355"),
          CosmeticId = cosmetics[0].Id,
          ImageUrl = "https://example.com/hydrating-face-cream.png"
        },
        new CosmeticImage
        {
          Id = new Guid("76AB322C-CD7C-4862-ADA1-15AE2AE78E84"),
          CosmeticId = cosmetics[1].Id,
          ImageUrl = "https://example.com/gentle-facial-cleanser.png"
        }
      };

      await context.CosmeticsImages.AddRangeAsync(cosmeticImages);
      await context.SaveChangesAsync();
    }
  }


  public static async Task SeedCosmeticSubCategoryAsync(ApplicationDbContext context)
  {
    if (!await context.CosmeticSubCategories.AnyAsync())
    {
      // Retrieve existing cosmetics and subcategories from the database
      var cosmetics = await context.Cosmetics.ToListAsync();
      var subCategories = await context.SubCategories.ToListAsync();

      // Ensure there are cosmetics and subcategories available
      if (cosmetics.Count < 2 || subCategories.Count < 3)
      {
        throw new Exception("Not enough cosmetics or subcategories have been seeded.");
      }

      var cosmeticSubCategories = new List<CosmeticSubCategory>
      {
        new CosmeticSubCategory { CosmeticId = cosmetics[0].Id, SubCategoryId = subCategories[0].Id },
        new CosmeticSubCategory { CosmeticId = cosmetics[0].Id, SubCategoryId = subCategories[2].Id },
        new CosmeticSubCategory { CosmeticId = cosmetics[1].Id, SubCategoryId = subCategories[1].Id }
      };

      await context.CosmeticSubCategories.AddRangeAsync(InitialData.CosmeticSubCategories);
      await context.SaveChangesAsync();
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

  public static async Task SeedFeedbackAsync(ApplicationDbContext context)
  {
    if (!await context.Feedbacks.AnyAsync())
    {
      // Retrieve existing users and cosmetics from the database
      var users = await context.Users.ToListAsync();
      var cosmetics = await context.Cosmetics.ToListAsync();

      // Ensure that there are enough users and cosmetics
      if (users.Count < 6 || cosmetics.Count < 2)
      {
        throw new Exception("Not enough users or cosmetics have been seeded.");
      }

      var feedbacks = new List<Feedback>
      {
        new Feedback
        {
          Id = new Guid("16784D61-99F6-4242-879D-AE16C088AE0D"),
          CosmeticId = cosmetics[0].Id,
          CustomerId = users[5].Id,
          Content = "This cleanser left my skin feeling incredibly soft and refreshed.",
          Rating = 4.5m
        },
        new Feedback
        {
          Id = new Guid("476FB2C8-476B-4426-B025-87BEE1AE00C6"),
          CosmeticId = cosmetics[1].Id,
          CustomerId = users[5].Id,
          Content = "The serum really brightened my complexion. Highly recommend!",
          Rating = 5.0m
        },
        new Feedback
        {
          Id = new Guid("F7D08F8A-4650-4320-89DE-FD790D992BF0"),
          CosmeticId = cosmetics[0].Id,
          CustomerId = users[5].Id,
          Content = "Decent product, but didn't meet all my expectations.",
          Rating = 3.5m
        }
      };

      await context.Feedbacks.AddRangeAsync(feedbacks);
      await context.SaveChangesAsync();
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

  public static async Task SeedRoutineStepAsync(ApplicationDbContext context)
  {
    if (!await context.RoutineSteps.AnyAsync())
    {
      // Retrieve existing routines and cosmetics from the database
      var routines = await context.Routines.ToListAsync();
      var cosmetics = await context.Cosmetics.ToListAsync();
      var steps = new List<RoutineStep>();

      foreach (var routine in routines)
      {
        var cleanser = cosmetics.FirstOrDefault(c =>
          c.Name.Contains("cleanser", StringComparison.OrdinalIgnoreCase) && c.SkinTypeId == routine.SkinTypeId);
        var moisturizer = cosmetics.FirstOrDefault(c =>
          c.Name.Contains("moisturizer", StringComparison.OrdinalIgnoreCase) && c.SkinTypeId == routine.SkinTypeId);
        var sunscreen = cosmetics.FirstOrDefault(c =>
          c.Name.Contains("sunscreen", StringComparison.OrdinalIgnoreCase) && c.SkinTypeId == routine.SkinTypeId);
        var retinoid = cosmetics.FirstOrDefault(c =>
          c.Name.Contains("retinoid", StringComparison.OrdinalIgnoreCase) && c.SkinTypeId == routine.SkinTypeId);

        if (routine.Period.Equals("Morning", StringComparison.OrdinalIgnoreCase))
        {
          // Morning Routine Steps
          if (cleanser != null)
          {
            steps.Add(new RoutineStep
            {
              Id = Guid.NewGuid(), RoutineId = routine.Id, CosmeticId = cleanser.Id, StepNumber = 1
            });
          }

          if (moisturizer != null)
          {
            steps.Add(new RoutineStep
            {
              Id = Guid.NewGuid(), RoutineId = routine.Id, CosmeticId = moisturizer.Id, StepNumber = 2
            });
          }

          if (sunscreen != null)
          {
            steps.Add(new RoutineStep
            {
              Id = Guid.NewGuid(), RoutineId = routine.Id, CosmeticId = sunscreen.Id, StepNumber = 3
            });
          }
        }
        else if (routine.Period.Equals("Evening", StringComparison.OrdinalIgnoreCase))
        {
          // Evening Routine Steps
          if (cleanser != null)
          {
            steps.Add(new RoutineStep
            {
              Id = Guid.NewGuid(), RoutineId = routine.Id, CosmeticId = cleanser.Id, StepNumber = 1
            });
          }

          if (moisturizer != null)
          {
            steps.Add(new RoutineStep
            {
              Id = Guid.NewGuid(), RoutineId = routine.Id, CosmeticId = moisturizer.Id, StepNumber = 2
            });
          }

          if (retinoid != null)
          {
            steps.Add(new RoutineStep
            {
              Id = Guid.NewGuid(), RoutineId = routine.Id, CosmeticId = retinoid.Id, StepNumber = 3
            });
          }
        }
      }

      await context.RoutineSteps.AddRangeAsync(InitialData.RoutineSteps);
      await context.SaveChangesAsync();
    }
  }


  private static async Task SeedSkinTypeAsync(ApplicationDbContext context)
  {
    if (!await context.SkinTypes.AnyAsync())
    {
      await context.SkinTypes.AddRangeAsync(InitialData.SkinTypes);
    }
  }

  public static async Task SeedSubCategoryAsync(ApplicationDbContext context)
  {
    if (!await context.SubCategories.AnyAsync())
    {
      // Retrieve existing categories from the database
      var categories = await context.Categories.ToListAsync();

      // Ensure that there are enough categories
      if (categories.Count < 8)
      {
        throw new Exception("Not enough categories have been seeded.");
      }

      var subCategories = new List<SubCategory>
      {
        // Radiance (Category 1)
        new SubCategory
        {
          Id = new Guid("FA5585BA-D79A-4738-973A-2A46BAAFEF02"),
          CategoryId = categories[0].Id,
          Name = "Glow Boosters",
          Description = "Enhance natural radiance and luminosity."
        },
        new SubCategory
        {
          Id = new Guid("924F9CB9-DA41-4B51-A9E6-3F43B15BF4A6"),
          CategoryId = categories[0].Id,
          Name = "Brightening Formulas",
          Description = "Products formulated to brighten and even out skin tone."
        },
        new SubCategory
        {
          Id = new Guid("9B4F16A4-3163-4744-9399-E9381819283A"),
          CategoryId = categories[0].Id,
          Name = "Illuminators",
          Description = "Subtle enhancers for a lit-from-within glow."
        },

        // Rejuvenation (Category 2)
        new SubCategory
        {
          Id = new Guid("A8EF488E-56C6-46AC-889B-4A0FFBD680AC"),
          CategoryId = categories[1].Id,
          Name = "Age-Defying Treatments",
          Description = "Reduce the signs of aging and smooth wrinkles."
        },
        new SubCategory
        {
          Id = new Guid("9A924542-D844-43C2-88CB-5641D00A22DD"),
          CategoryId = categories[1].Id,
          Name = "Firming Solutions",
          Description = "Products that help to tighten and firm the skin."
        },
        new SubCategory
        {
          Id = new Guid("277169B9-E26C-4EE1-9CBC-B4E326E82EE0"),
          CategoryId = categories[1].Id,
          Name = "Renewal Complex",
          Description = "Promote cell turnover and skin renewal."
        },

        // Purity (Category 3)
        new SubCategory
        {
          Id = new Guid("C7C096CE-8959-494D-8913-4139CA48B160"),
          CategoryId = categories[2].Id,
          Name = "Deep Cleansing",
          Description = "Thorough cleansers to purify the skin."
        },
        new SubCategory
        {
          Id = new Guid("FA60A320-490A-4CCF-8BA8-1386DF682A61"),
          CategoryId = categories[2].Id,
          Name = "Detox & Clarify",
          Description = "Remove impurities and unclog pores."
        },
        new SubCategory
        {
          Id = new Guid("17E228DD-F780-4CCE-8884-DFE530FD5A00"),
          CategoryId = categories[2].Id,
          Name = "Pore Refiners",
          Description = "Minimize pores and smooth skin texture."
        },

        // Hydration (Category 4)
        new SubCategory
        {
          Id = new Guid("7FB4A783-8587-4A18-A0DA-94D40682EEFC"),
          CategoryId = categories[3].Id,
          Name = "Moisture Locks",
          Description = "Seal in hydration for long-lasting moisture."
        },
        new SubCategory
        {
          Id = new Guid("F86FA2D9-B3C1-48DF-828E-4BD6CDD76319"),
          CategoryId = categories[3].Id,
          Name = "Hydrating Essentials",
          Description = "Fundamental products for daily hydration."
        },
        new SubCategory
        {
          Id = new Guid("C6A71C5D-A067-4BB6-8D9C-3CA3F180ADFD"),
          CategoryId = categories[3].Id,
          Name = "Nourishing Creams",
          Description = "Rich creams that deeply nourish the skin."
        },

        // Balance (Category 5)
        new SubCategory
        {
          Id = new Guid("71895946-0B7C-40F9-97E0-9195D7BDC5E2"),
          CategoryId = categories[4].Id,
          Name = "pH Balancing",
          Description = "Products to maintain the skin's natural pH."
        },
        new SubCategory
        {
          Id = new Guid("9AD8FBB8-DB09-4842-95B3-590F3728ED42"),
          CategoryId = categories[4].Id,
          Name = "Soothing Solutions",
          Description = "Calm and reduce irritation for balanced skin."
        },

        // Protection (Category 6)
        new SubCategory
        {
          Id = new Guid("C4A28A94-14DA-452D-96EB-1B01D4892C84"),
          CategoryId = categories[5].Id,
          Name = "Environmental Shields",
          Description = "Defend skin against pollution and external stressors."
        },
        new SubCategory
        {
          Id = new Guid("39783437-38B8-480A-B9A5-4814E9920C36"),
          CategoryId = categories[5].Id,
          Name = "SPF Essentials",
          Description = "Sunscreens and UV protective formulations."
        },

        // Specialized Care (Category 7)
        new SubCategory
        {
          Id = new Guid("7B314245-8C44-4E8D-B763-155291ED57C8"),
          CategoryId = categories[6].Id,
          Name = "Targeted Remedies",
          Description = "Specific solutions for defined skin issues."
        },
        new SubCategory
        {
          Id = new Guid("646E7856-BF86-46FB-A71A-799FDAC30F82"),
          CategoryId = categories[6].Id,
          Name = "Delicate Area Care",
          Description = "Gentle products for sensitive areas like eyes and lips."
        },

        // Masking (Category 8)
        new SubCategory
        {
          Id = new Guid("93894942-ADB7-4D19-ABBF-1AA6620B5BCB"),
          CategoryId = categories[7].Id,
          Name = "Sheet Masks",
          Description = "Single-use masks for an instant boost."
        },
        new SubCategory
        {
          Id = new Guid("DADC109E-FA2E-4BE0-A5E2-29FC46F54826"),
          CategoryId = categories[7].Id,
          Name = "Overnight Masks",
          Description = "Leave-on treatments for intensive overnight care."
        }
      };

      await context.SubCategories.AddRangeAsync(InitialData.SubCategories);
      await context.SaveChangesAsync();
    }
  }


  private static async Task SeedTagAsync(ApplicationDbContext context)
  {
    if (!await context.Tags.AnyAsync())
    {
      await context.Tags.AddRangeAsync(InitialData.Tags);
      await context.SaveChangesAsync();
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