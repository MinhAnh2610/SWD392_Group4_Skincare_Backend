namespace CleanArchitecture.Infrastructure.Data.Extensions;

internal class InitialData
{
  public static IEnumerable<User> Users => new List<User>
  {
    new()
    {
      Id = new Guid("8E62D9EE-64AF-47E7-8A49-FA1E2F5DA054"),
      UserName = "Admin123",
      Email = "admin123@gmail.com",
      EmailConfirmed = true,
      BirthDate = new DateOnly(2000, 1, 1),
      FirstName = "John",
      LastName = "Doe",
      Gender = true,
      PhoneNumber = "1234567890"
    },
    new()
    {
      Id = new Guid("D650E6F2-C402-45C1-B92F-F7573BDE7335"),
      UserName = "MinhAnh2610",
      Email = "minhanh26102004@gmail.com",
      EmailConfirmed = true,
      BirthDate = new DateOnly(2000,1,1),
      FirstName = "Pham",
      LastName = "Anh",
      Gender = true,
      PhoneNumber = "1234567890"
    },
    new User
    {
      Id = new Guid("103E55BA-607A-4D27-8119-B0C22969F02A"),
      UserName = "AliceJ",
      FirstName = "Alice",
      LastName = "Johnson",
      BirthDate = new DateOnly(1985, 6, 15),
      Gender = true,
      Email = "alice.johnson@example.com",
      PhoneNumber = "1234567890"
    },
    new User
    {
      Id = new Guid("518F5719-3E70-4AE2-BD92-B7FDC7A84DA2"),
      UserName = "BobS",
      FirstName = "Bob",
      LastName = "Smith",
      BirthDate = new DateOnly(1990, 3, 22),
      Gender = true,
      Email = "bob.smith@example.com",
      PhoneNumber = "1234567890"
    },
    new User
    {
      Id = new Guid("70FF9BBF-BD8D-46BE-AE26-772C74973276"),
      UserName = "CharlieD",
      FirstName = "Charlie",
      LastName = "Davis",
      BirthDate = new DateOnly(1988, 9, 10),
      Gender = true,
      Email = "charlie.davis@example.com",
      PhoneNumber = "1234567890"
    },
    new User
    {
      Id = new Guid("A1E34814-7E85-4858-B0AB-2D9E2DA27D99"),
      UserName = "EmmaW",
      FirstName = "Emma",
      LastName = "Williams",
      BirthDate = new DateOnly(1995, 12, 5),
      Gender = false,
      Email = "emma.williams@example.com",
      PhoneNumber = "1234567890"
    },
    new User
    {
      Id = new Guid("2AD907D6-65D9-4A8F-B662-FA5EDF293FC6"),
      UserName = "DanielB",
      FirstName = "Daniel",
      LastName = "Brown",
      BirthDate = new DateOnly(2000, 7, 19),
      Gender = true,
      Email = "daniel.brown@example.com",
      PhoneNumber = "1234567890"
    }
  };

  public static IEnumerable<Role> Roles => new List<Role>
  {
    new Role { Id = new Guid("55535483-D1EC-459C-8189-C6D8C4B50195"), Name = "Customer" },
    new Role { Id = new Guid("C459402C-64CB-4D36-9238-60B15462CE25"), Name = "Staff" },
    new Role { Id = new Guid("D586839A-B80C-4EBB-8488-95CDD857D1BF"), Name = "Manager" }
  };

  public static IEnumerable<Batch> Batches
  {
    get
    {
      var cosmetics = Cosmetics.ToList();
      return new List<Batch>
      {
        new Batch
        {
          Id = new Guid("8342CF0F-182F-4B7E-963D-6F4061939B9C"),
          CosmeticId = cosmetics[0].Id,
          Quantity = 100,
          ManufactureDate = new DateOnly(2025, 1, 1),
          ExportedDate = new DateOnly(2025, 2, 1),
          ExpirationDate = new DateOnly(2026, 1, 1),
        },
        new Batch
        {
          Id = new Guid("96F6C0EA-FB8E-4DFB-947A-A77027006B36"),
          CosmeticId = cosmetics[1].Id,
          Quantity = 200,
          ManufactureDate = new DateOnly(2025, 3, 15),
          ExportedDate = new DateOnly(2025, 4, 1),
          ExpirationDate = new DateOnly(2027, 3, 15),
        },
      };
    }
  }

  public static IEnumerable<Blog> Blogs
  {
    get
    {
      var staffs = Users.ToList();
      return new List<Blog>
      {
        new Blog
        {
          Id = new Guid("CB64A508-526E-4156-A512-1D1BF7A5A032"),
          StaffId = staffs[3].Id,
          Title = "The Future of AI",
          Content = "AI is transforming industries...",
          BlogTags = new List<BlogTag>() // BlogTags will be assigned later
        },
        new Blog
        {
          Id = new Guid("C9DED966-A2F7-4328-B899-8201508D5476"),
          StaffId = staffs[3].Id,
          Title = "Starting a Business in 2024",
          Content = "Starting a business has never been easier...",
          BlogTags = new List<BlogTag>()
        }
      };
    }
  }

  public static IEnumerable<BlogTag> BlogTags
  {
    get
    {
      var blogs = Blogs.ToList();
      var tags = Tags.ToList();

      return new List<BlogTag>
      {
        new BlogTag { BlogId = blogs[0].Id, TagId = tags[0].Id }, // AI & Tech
        new BlogTag { BlogId = blogs[1].Id, TagId = tags[1].Id }  // Business  
      };
    }
  }

  public static IEnumerable<Brand> Brands => new List<Brand>
  {
    new Brand { Id = new Guid("9286DAB7-CCA8-4C04-9D2D-FF1072A1746A"), Name = "L'Oreal", Description = "Global beauty brand", WebsiteUrl = "https://www.loreal.com", LogoUrl = "https://example.com/loreal-logo.png" },
    new Brand { Id = new Guid("A6ACE76F-5DA7-4EEB-A909-49B0390F34A2"), Name = "Clinique", Description = "Fragrance-free skincare products", WebsiteUrl = "https://www.clinique.com", LogoUrl = "https://example.com/clinique-logo.png" },
    new Brand { Id = new Guid("735B21D1-AB56-45BD-8404-7722112AE8E2"), Name = "Neutrogena", Description = "Dermatologist-recommended skincare", WebsiteUrl = "https://www.neutrogena.com", LogoUrl = "https://example.com/neutrogena-logo.png" },
    new Brand { Id = new Guid("B4D858D8-28D6-476A-9BDF-B9265DA3E160"), Name = "CeraVe", Description = "Skincare with essential ceramides", WebsiteUrl = "https://www.cerave.com", LogoUrl = "https://example.com/cerave-logo.png" },
    new Brand { Id = new Guid("0F7ABD8F-0905-4EDE-A573-C46B6EF86267"), Name = "The Ordinary", Description = "Clinical formulations with integrity", WebsiteUrl = "https://www.theordinary.com", LogoUrl = "https://example.com/theordinary-logo.png" }
  };

  public static IEnumerable<Cart> Carts
  {
    get
    {
      var customers = Users.ToList();
      return new List<Cart>
      {
        new Cart
        {
          Id = customers[5].Id,
          TotalPrice = 89.97m
        }
      };
    }
  }

  public static IEnumerable<CartItem> CartItems
  {
    get
    {
      var customer = Users.ToList();
      var cosmetics = Cosmetics.ToList();
      return new List<CartItem>
      {
        new CartItem
        {
          CosmeticId = cosmetics[0].Id,
          Quantity = 2
        },
        new CartItem
        {
          CosmeticId = cosmetics[1].Id,
          Quantity = 1
        }
      };
    }
  }

  public static IEnumerable<Category> Categories => new List<Category>
  {
    new Category
    {
      Id = new Guid("0419D5D4-1C6A-4F2D-ACFA-D3E5687C5A73"),
      Name = "Radiance",
      Description = "Enhance your natural glow and brighten your complexion."
    },
    new Category
    {
      Id = new Guid("0CDF8B34-6D34-4669-9757-10C68A69ECAA"),
      Name = "Rejuvenation",
      Description = "Age-defying and skin renewal solutions."
    },
    new Category
    {
      Id = new Guid("222AFB3A-879D-4FB1-AEB4-BCD4EECE09C4"),
      Name = "Purity",
      Description = "Deep cleansing and detoxifying products."
    },
    new Category
    {
      Id = new Guid("ECBE8150-1FF8-4DE8-B13F-44E007E2487D"),
      Name = "Hydration",
      Description = "Products to lock in moisture and nourish your skin."
    },
    new Category
    {
      Id = new Guid("E187FB64-B583-4BF6-A9F7-34C8DCE1CDCF"),
      Name = "Balance",
      Description = "Formulations to maintain skin pH and soothe irritation."
    },
    new Category
    {
      Id = new Guid("16C105C4-D38D-46A1-98CA-48852724DA8F"),
      Name = "Protection",
      Description = "Guard your skin against environmental stressors."
    },
    new Category
    {
      Id = new Guid("67D4B89F-17EE-4458-9678-6454E89547DD"),
      Name = "Specialized Care",
      Description = "Targeted solutions for specific skin concerns."
    },
    new Category
    {
      Id = new Guid("D5E75588-1D27-4FF4-8DDA-6220DF581268"),
      Name = "Masking",
      Description = "Intensive treatments delivered via masks."
    }
  };

  public static IEnumerable<CosmeticType> CosmeticTypes => new List<CosmeticType>
  {
    new CosmeticType
    {
      Id = new Guid("AC635372-0D15-44F7-BF5F-7496C4F16051"),
      Name = "Cleansers",
      Description = "Products that clean the skin."
    },
    new CosmeticType
    {
      Id = new Guid("119B5F44-626F-4831-8978-388A8B2AD23C"),
      Name = "Exfoliators",
      Description = "Products to remove dead skin cells."
    },
    new CosmeticType
    {
      Id = new Guid("A0F0DF4B-07C2-4984-A914-184479DB31B0"),
      Name = "Toners",
      Description = "Products to balance and refresh the skin."
    },
    new CosmeticType
    {
      Id = new Guid("22D4240D-9674-4E6C-B0B7-72F84CA010B8"),
      Name = "Serums",
      Description = "Concentrated treatments for skin."
    },
    new CosmeticType
    {
      Id = new Guid("4CCDED92-BB02-4B8E-9119-95F59628C5D2"),
      Name = "Moisturizers",
      Description = "Products to hydrate and nourish the skin."
    },
    new CosmeticType
    {
      Id = new Guid("79406AF7-46EE-49B4-9D4C-6812A2C396A0"),
      Name = "Eye Creams",
      Description = "Specialized creams for the eye area."
    },
    new CosmeticType
    {
      Id = new Guid("79BD1C90-FAF6-4A08-9893-DBA2552FEE68"),
      Name = "Sunscreens",
      Description = "Products that protect the skin from UV rays."
    },
    new CosmeticType
    {
      Id = new Guid("C6C92631-D452-4C16-A065-3FA93CE750D8"),
      Name = "Lip Care Products",
      Description = "Products to care for and enhance the lips."
    },
    new CosmeticType
    {
      Id = new Guid("C4FDAAE7-B369-4842-B31B-37EAA3072534"),
      Name = "Face Masks",
      Description = "Treatments that refresh and rejuvenate the skin."
    }
  };

  public static IEnumerable<Cosmetic> Cosmetics
  {
    get
    {
      var brands = Brands.ToList();
      var skinTypes = SkinTypes.ToList();
      var cosmeticTypes = CosmeticTypes.ToList();
      return new List<Cosmetic>
      {
        new Cosmetic
        {
          Id = new Guid("A14E2C76-662E-4FA4-A9A9-81EE4512D441"),
          Name = "Hydrating Face Cream",
          BrandId = brands[0].Id,
          SkinTypeId = skinTypes[0].Id,
          CosmeticTypeId = cosmeticTypes[0].Id,
          Price = 34.99m,
          Gender = true,
          Notice = "Apply twice daily for best results.",
          Ingredients = "Water, Hyaluronic Acid, Glycerin",
          MainUsage = "Moisturizing and hydrating",
          Texture = "Cream",
          Origin = "France",
          Instructions = "Apply on a cleansed face in the morning and at night."
        },
        new Cosmetic
        {
          Id = new Guid("70DEC3C1-B8FB-4BCB-AA27-B04B49E5D626"),
          Name = "Gentle Facial Cleanser",
          BrandId = brands[1].Id,
          SkinTypeId = skinTypes[1].Id,
          CosmeticTypeId = cosmeticTypes[1].Id,
          Price = 19.99m,
          Gender = true,
          Notice = "Suitable for daily use.",
          Ingredients = "Salicylic Acid, Chamomile Extract",
          MainUsage = "Cleansing and gentle exfoliation",
          Texture = "Gel",
          Origin = "USA",
          Instructions = "Massage onto wet skin and rinse thoroughly."
        },
        new Cosmetic
        {
          Id = new Guid("1D3E5EC9-6FEC-450E-8283-D44D349EDBBF"),
          Name = "Revitalizing Serum",
          BrandId = brands[1].Id,
          SkinTypeId = skinTypes[1].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Serums").Id,
          Price = 34.99m,
          Gender = true,
          Notice = "Apply a few drops after cleansing.",
          Ingredients = "Hyaluronic Acid, Vitamin C, Peptides",
          MainUsage = "Revitalizing and rejuvenating the skin",
          Texture = "Light Gel",
          Origin = "France",
          Instructions = "Apply 2-3 drops on cleansed skin, morning and night."
        },
        new Cosmetic
        {
          Id = new Guid("3A7DFA4B-E4C7-430D-9B0B-AC2E341602C4"),
          Name = "Hydrating Moisturizer",
          BrandId = brands[0].Id,
          SkinTypeId = skinTypes[2].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Moisturizers").Id,
          Price = 29.99m,
          Gender = true,
          Notice = "Locks in moisture for all-day hydration.",
          Ingredients = "Glycerin, Hyaluronic Acid, Ceramides",
          MainUsage = "Hydration and nourishment",
          Texture = "Cream",
          Origin = "Germany",
          Instructions = "Apply evenly to face and neck after cleansing."
        },
        new Cosmetic
        {
          Id = new Guid("8877B094-53E7-4A6E-8A10-7A47A805311F"),
          Name = "Soothing Toner",
          BrandId = brands[1].Id,
          SkinTypeId = skinTypes[0].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Toners").Id,
          Price = 14.99m,
          Gender = true,
          Notice = "Balances skin pH and preps skin for moisturizer.",
          Ingredients = "Witch Hazel, Aloe Vera, Glycerin",
          MainUsage = "Toning and refreshing the skin",
          Texture = "Liquid",
          Origin = "USA",
          Instructions = "Apply with a cotton pad after cleansing."
        },
        new Cosmetic
        {
          Id = new Guid("2125C657-3ACA-4960-B4C5-DA0DB91B1968"),
          Name = "Exfoliating Scrub",
          BrandId = brands[0].Id,
          SkinTypeId = skinTypes[0].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Exfoliators").Id,
          Price = 24.99m,
          Gender = true,
          Notice = "Use 2-3 times a week for smooth skin.",
          Ingredients = "Sugar, Jojoba Beads, Natural Extracts",
          MainUsage = "Exfoliation and smoothing skin texture",
          Texture = "Scrub",
          Origin = "USA",
          Instructions = "Gently massage on damp skin and rinse off."
        },
        new Cosmetic
        {
          Id = new Guid("3F0FE941-D7F6-433D-AD8E-F0003AAAA75C"),
          Name = "Nourishing Night Cream",
          BrandId = brands[1].Id,
          SkinTypeId = skinTypes[2].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Moisturizers").Id,
          Price = 39.99m,
          Gender = true,
          Notice = "For overnight skin repair and nourishment.",
          Ingredients = "Peptides, Antioxidants, Hyaluronic Acid",
          MainUsage = "Deep hydration and skin regeneration",
          Texture = "Rich Cream",
          Origin = "Italy",
          Instructions = "Apply evenly to clean skin before bed."
        },
        new Cosmetic
        {
          Id = new Guid("AF48F533-2AF5-4A28-BEFD-24CB3F851218"),
          Name = "Brightening Eye Cream",
          BrandId = brands[0].Id,
          SkinTypeId = skinTypes[0].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Eye Creams").Id,
          Price = 27.99m,
          Gender = true,
          Notice = "Reduces dark circles and puffiness.",
          Ingredients = "Caffeine, Vitamin E, Peptides",
          MainUsage = "Brightening and firming the eye area",
          Texture = "Gel-Cream",
          Origin = "France",
          Instructions = "Gently tap around the eye area using your ring finger."
        },
        new Cosmetic
        {
          Id = new Guid("103ED5BE-D2D4-4C24-8DF9-B854CAAE48D1"),
          Name = "UV Protection Sunscreen",
          BrandId = brands[1].Id,
          SkinTypeId = skinTypes[2].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Sunscreens").Id,
          Price = 21.99m,
          Gender = true,
          Notice = "Broad spectrum SPF 50 for daily protection.",
          Ingredients = "Zinc Oxide, Titanium Dioxide, Vitamin E",
          MainUsage = "Sun protection and prevention of premature aging",
          Texture = "Lotion",
          Origin = "USA",
          Instructions = "Apply generously 15 minutes before sun exposure."
        },
        new Cosmetic
        {
          Id = new Guid("CBEDBE89-9EC6-4487-9481-789A90575B3A"),
          Name = "Soothing Lip Balm",
          BrandId = brands[0].Id,
          SkinTypeId = skinTypes[0].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Lip Care Products").Id,
          Price = 9.99m,
          Gender = true,
          Notice = "Keeps lips moisturized and smooth.",
          Ingredients = "Beeswax, Shea Butter, Vitamin E",
          MainUsage = "Lip hydration and care",
          Texture = "Balm",
          Origin = "USA",
          Instructions = "Apply to lips as needed throughout the day."
        },
        new Cosmetic
        {
          Id = new Guid("3AF3131D-6996-4EC7-8FEC-64A53C8B3C7E"),
          Name = "Hydrating Face Mask",
          BrandId = brands[1].Id,
          SkinTypeId = skinTypes[2].Id,
          CosmeticTypeId = cosmeticTypes.First(ct => ct.Name == "Face Masks").Id,
          Price = 16.99m,
          Gender = true,
          Notice = "Instant hydration boost.",
          Ingredients = "Aloe, Hyaluronic Acid, Vitamins",
          MainUsage = "Hydration and revitalization",
          Texture = "Sheet Mask",
          Origin = "South Korea",
          Instructions = "Apply for 15-20 minutes, then remove and pat remaining essence."
        }
      };
    }
  }

  public static IEnumerable<CosmeticImage> CosmeticImages
  {
    get
    {
      var cosmetics = Cosmetics.ToList();
      return new List<CosmeticImage>
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
    }
  }

  public static IEnumerable<CosmeticSubCategory> CosmeticSubCategories
  {
    get
    {
      var cosmetics = Cosmetics.ToList();
      var subCategories = SubCategories.ToList();
      return new List<CosmeticSubCategory>
      {
        new CosmeticSubCategory
        {
          CosmeticId = cosmetics[0].Id,
          SubCategoryId = subCategories[0].Id
        },
        new CosmeticSubCategory
        {
          CosmeticId = cosmetics[0].Id,
          SubCategoryId = subCategories[2].Id
        },
        new CosmeticSubCategory
        {
          CosmeticId = cosmetics[1].Id,
          SubCategoryId = subCategories[1].Id
        }
      };
    }
  }

  public static IEnumerable<CompanyInformation> CompanyInfos => new List<CompanyInformation>
  {
    new CompanyInformation
    {
      Id = new Guid("DD7C5D83-2B34-4943-87EC-95D965C13FF9"),
      Name = "TechCorp",
      Description = "A leading technology company.",
      LogoUrl = "https://example.com/logo.png",
      Email = "contact@techcorp.com",
      PhoneNumber = "+123456789",
      Address = "123 Tech Street, Silicon Valley, CA"
    }
  };

  public static IEnumerable<Coupon> Coupons => new List<Coupon>
  {
    new Coupon
    {
      Id = new Guid("6E8F40E3-7A19-4A41-B3F8-4DFF00FD8C21"),
      Code = "DISCOUNT10",
      DiscountAmount = 10.00
    }
  };

  public static IEnumerable<FAQ> FAQs => new List<FAQ>
  {
    new FAQ { Id = new Guid("2B4AE24C-EED6-4EBC-AFD9-5DA8E22037F3"), Question = "How do I reset my password?", Answer = "You can reset your password in the settings page." },
    new FAQ { Id = new Guid("21AE2A57-761C-4A36-B5FF-62488BF8F4FB"), Question = "Where can I contact support?", Answer = "You can contact support at support@example.com." }
  };

  public static IEnumerable<Feedback> Feedbacks
  {
    get
    {
      var users = Users.ToList();
      var cosmetics = Cosmetics.ToList();
      return new List<Feedback>
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
    }
  }

  public static IEnumerable<Order> Orders
  {
    get
    {
      var customer = Users.ToList();
      var coupon = Coupons.First();
      return new List<Order>
      {
        new Order
        {
          Id = new Guid("F72B5E3B-C443-4920-9D03-BB774721F70A"),
          CustomerId = customer[5].Id,
          CouponId = coupon.Id,
          SubTotal = 89.97m,
          TotalPrice = 79.97m,
          OrderDate = DateTime.UtcNow,
          ShippingAddress = "123 Main Street, City, Country",
          BillingAddress = "123 Main Street, City, Country",
          TrackingNumber = "TRACK123",
          DeliveryDate = DateTime.UtcNow.AddDays(5),
          Status = "Processing"
        }
      };
    }
  }

  public static IEnumerable<OrderItem> OrderItems
  {
    get
    {
      var order = Orders.First();
      var cosmetics = Cosmetics.ToList();
      return new List<OrderItem>
      {
        new OrderItem
        {
          OrderId = order.Id,
          CosmeticId = cosmetics[0].Id,
          Quantity = 2
        },
        new OrderItem
        {
          OrderId = order.Id,
          CosmeticId = cosmetics[1].Id,
          Quantity = 1
        }
      };
    }
  }

  public static IEnumerable<Policy> Policies => new List<Policy>
  {
    new Policy { Id = new Guid("70B1B2A8-8361-468C-A602-04E1A815495E"), Title = "Privacy Policy", Content = "This is the privacy policy content." },
    new Policy { Id = new Guid("EC19577C-8D19-4497-801A-11B66E55666D"), Title = "Terms of Service", Content = "These are the terms of service." }
  };

  public static IEnumerable<Routine> Routines
  {
    get
    {
      var skinTypes = SkinTypes.ToList();
      var morningRoutine = new Routine
      {
        Id = new Guid("A453329C-9635-4DC9-82A5-5D50883F52C9"),
        SkinTypeId = skinTypes[0].Id,
        Title = "Morning Routine",
        Period = "Morning",
      };

      var eveningRoutine = new Routine
      {
        Id = new Guid("A194BD2B-2BCD-4CB1-9E28-45F9F4C5CD85"),
        SkinTypeId = skinTypes[13].Id,
        Title = "Evening Routine",
        Period = "Evening",
      };

      return new List<Routine> { morningRoutine, eveningRoutine };
    }
  }

  public static IEnumerable<RoutineStep> RoutineSteps
  {
    get
    {
      var routines = Routines.ToList();
      var cosmetics = Cosmetics.ToList();
      var steps = new List<RoutineStep>
      {
        // Morning Routine Steps (for oily, sensitive skin)
        new RoutineStep
        {
          Id = new Guid("A64ACFD4-2352-4A5F-AF71-F010549139F8"),
          RoutineId = routines[0].Id,
          CosmeticId = cosmetics[1].Id,
          StepNumber = 1
        },
        new RoutineStep
        {
          Id = new Guid("A26909AB-7E98-4241-8967-5C1099C1D12B"),
          RoutineId = routines[0].Id,
          CosmeticId = cosmetics[4].Id,
          StepNumber = 2
        },
        new RoutineStep
        {
          Id = new Guid("92867F27-6111-4DE4-A4A3-50A28031BDE6"),
          RoutineId = routines[0].Id,
          CosmeticId = cosmetics[2].Id,
          StepNumber = 3
        },
        new RoutineStep
        {
          Id = new Guid("4C7362B9-D2C3-4013-8637-94AD06193845"),
          RoutineId = routines[0].Id,
          CosmeticId = cosmetics[3].Id,
          StepNumber = 4
        },

        // Evening Routine Steps (for dry, sensitive skin)
        new RoutineStep
        {
          Id = new Guid("BCDCA4D0-45BB-4074-B0DD-E50D5FF4A823"),
          RoutineId = routines[1].Id,
          CosmeticId = cosmetics[1].Id,
          StepNumber = 1
        },
        new RoutineStep
        {
          Id = new Guid("26EFC8B8-3139-4EEA-8442-2BABC1D54326"),
          RoutineId = routines[1].Id,
          CosmeticId = cosmetics[2].Id,
          StepNumber = 2
        },
        new RoutineStep
        {
          Id = new Guid("5EBF2F4F-78EB-4159-9780-47D01D1826FD"),
          RoutineId = routines[1].Id,
          CosmeticId = cosmetics[3].Id,
          StepNumber = 3
        }
      };
      return steps;
    }
  }

  public static IEnumerable<SkinType> SkinTypes => new List<SkinType>
  {
    new SkinType { Id = new Guid("F73B3596-D3F0-4DA9-BA46-0E75D332658B"), Name = "OSPW", Description = "Oily, Sensitive, Pigmented, Wrinkle-Prone", IsDry = false, IsSensitive = true, IsUneven = true, IsWrinkle = true },
    new SkinType { Id = new Guid("D7BCD97E-BE00-4CFC-83DE-1BCCE52F2CE3"), Name = "OSPT", Description = "Oily, Sensitive, Pigmented, Tight", IsDry = false, IsSensitive = true, IsUneven = true, IsWrinkle = false },
    new SkinType { Id = new Guid("20B72075-FE5F-429E-A75B-2C3DDF705A9C"), Name = "OSNW", Description = "Oily, Sensitive, Non-Pigmented, Wrinkle-Prone", IsDry = false, IsSensitive = true, IsUneven = false, IsWrinkle = true },
    new SkinType { Id = new Guid("E9307F73-A099-4930-8A76-1214F0E006DC"), Name = "OSNT", Description = "Oily, Sensitive, Non-Pigmented, Tight", IsDry = false, IsSensitive = true, IsUneven = false, IsWrinkle = false },
    new SkinType { Id = new Guid("F20DFE0E-3C5B-463A-8C28-7E54B3B62CCB"), Name = "ORPW", Description = "Oily, Resistant, Pigmented, Wrinkle-Prone", IsDry = false, IsSensitive = false, IsUneven = true, IsWrinkle = true },
    new SkinType { Id = new Guid("90700FA6-65DF-4D5D-93CF-1D5A6E9DA668"), Name = "ORPT", Description = "Oily, Resistant, Pigmented, Tight", IsDry = false, IsSensitive = false, IsUneven = true, IsWrinkle = false },
    new SkinType { Id = new Guid("210E8376-E992-4981-ACB8-C396B5DE20A3"), Name = "ORNW", Description = "Oily, Resistant, Non-Pigmented, Wrinkle-Prone", IsDry = false, IsSensitive = false, IsUneven = false, IsWrinkle = true },
    new SkinType { Id = new Guid("331EC094-8847-436B-9C79-A7B2D28F4B42"), Name = "ORNT", Description = "Oily, Resistant, Non-Pigmented, Tight", IsDry = false, IsSensitive = false, IsUneven = false, IsWrinkle = false },
    new SkinType { Id = new Guid("D412BC64-2134-40AD-9556-54F31B87A96E"), Name = "DSPW", Description = "Dry, Sensitive, Pigmented, Wrinkle-Prone", IsDry = true, IsSensitive = true, IsUneven = true, IsWrinkle = true },
    new SkinType { Id = new Guid("FEE794FC-41B9-4741-8774-21116EC8B3D3"), Name = "DSPT", Description = "Dry, Sensitive, Pigmented, Tight", IsDry = true, IsSensitive = true, IsUneven = true, IsWrinkle = false },
    new SkinType { Id = new Guid("63B38920-67FF-43E2-A9DD-95B989D064B4"), Name = "DSNW", Description = "Dry, Sensitive, Non-Pigmented, Wrinkle-Prone", IsDry = true, IsSensitive = true, IsUneven = false, IsWrinkle = true },
    new SkinType { Id = new Guid("7AC046BE-36EC-4DCF-AF8E-88DEFEE5023A"), Name = "DSNT", Description = "Dry, Sensitive, Non-Pigmented, Tight", IsDry = true, IsSensitive = true, IsUneven = false, IsWrinkle = false },
    new SkinType { Id = new Guid("2FC2B67E-D050-422D-BAE2-318A243FADDA"), Name = "DRPW", Description = "Dry, Resistant, Pigmented, Wrinkle-Prone", IsDry = true, IsSensitive = false, IsUneven = true, IsWrinkle = true },
    new SkinType { Id = new Guid("63E671F4-E686-4C46-B4DE-EB9B19319916"), Name = "DRPT", Description = "Dry, Resistant, Pigmented, Tight", IsDry = true, IsSensitive = false, IsUneven = true, IsWrinkle = false },
    new SkinType { Id = new Guid("13310480-17DA-405D-B4B4-ACBAED5A5E7A"), Name = "DRNW", Description = "Dry, Resistant, Non-Pigmented, Wrinkle-Prone", IsDry = true, IsSensitive = false, IsUneven = false, IsWrinkle = true },
    new SkinType { Id = new Guid("F585889E-076E-434A-A47F-AC952736712C"), Name = "DRNT", Description = "Dry, Resistant, Non-Pigmented, Tight", IsDry = true, IsSensitive = false, IsUneven = false, IsWrinkle = false }
  };

  public static IEnumerable<SubCategory> SubCategories
  {
    get
    {
      var categories = new List<Category>(Categories);

      return new List<SubCategory>
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
    }
  }

  public static IEnumerable<Tag> Tags => new List<Tag>
        {
            new Tag { Id = new Guid("4C592F29-29A6-49F7-B9FF-1EC76D36826A"), Name = "Technology", Description = "Posts related to technology" },
            new Tag { Id = new Guid("555B8613-1FC3-465C-8020-2C1E6E8D4668"), Name = "Business", Description = "Posts related to business" },
            new Tag { Id = new Guid("0DD9E52B-443F-4512-B44C-BC79E90B36CE"), Name = "Health", Description = "Posts related to health and wellness" }
        };

  public static IEnumerable<Testimonial> Testimonials => new List<Testimonial>
  {
    new Testimonial
    {
      Id = new Guid("A7804842-3B9E-47A0-9D0D-B464203D5C62"),
      CustomerId = new Guid("A1E34814-7E85-4858-B0AB-2D9E2DA27D99"),
      Content = "This product changed my life! I can't recommend it enough.",
      Rating = 5
    },
    new Testimonial
    {
      Id = new Guid("80E56A47-A71D-4B9C-AD7B-061FADAE4EA2"),
      CustomerId = new Guid("A1E34814-7E85-4858-B0AB-2D9E2DA27D99"),
      Content = "Great quality and fast shipping. Very satisfied.",
      Rating = 4
    },
    new Testimonial
    {
      Id = new Guid("A073408F-52CD-4C3C-87FF-5EDDC32C0AB8"),
      CustomerId = new Guid("A1E34814-7E85-4858-B0AB-2D9E2DA27D99"),
      Content = "Not what I expected, but still a decent product.",
      Rating = 3
    }
  };
}
