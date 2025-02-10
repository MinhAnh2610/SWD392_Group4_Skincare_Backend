namespace CleanArchitecture.Infrastructure.Data.Extensions;

internal class InitialData
{
  public static IEnumerable<User> Users =>
    new List<User>
    {
      new()
      {
        Id = Guid.NewGuid(),
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
        Id = Guid.NewGuid(),
        UserName = "MinhAnh2610",
        Email = "minhanh26102004@gmail.com",
        EmailConfirmed = true,
        BirthDate = new DateOnly(2000,1,1),
        FirstName = "Pham",
        LastName = "Anh",
        Gender = true,
        PhoneNumber = "1234567890"
      }
    };

  public static IEnumerable<Role> Roles =>
    new List<Role>
    {
      new() { Id = Guid.NewGuid(), Name = "Admin" },
      new() { Id = Guid.NewGuid(), Name = "Customer" }
    };

  public static IEnumerable<Policy> Policies => new List<Policy>
  {
    new Policy { Id = Guid.NewGuid(), Title = "Privacy Policy", Content = "This is the privacy policy content." },
    new Policy { Id = Guid.NewGuid(), Title = "Terms of Service", Content = "These are the terms of service." }
  };

  public static IEnumerable<FAQ> FAQs => new List<FAQ>
  {
    new FAQ { Id = Guid.NewGuid(), Question = "How do I reset my password?", Answer = "You can reset your password in the settings page." },
    new FAQ { Id = Guid.NewGuid(), Question = "Where can I contact support?", Answer = "You can contact support at support@example.com." }
  };

  public static IEnumerable<CompanyInformation> CompanyInfos => new List<CompanyInformation>
  {
    new CompanyInformation
    {
      Id = Guid.NewGuid(),
      Name = "TechCorp",
      Description = "A leading technology company.",
      LogoUrl = "https://example.com/logo.png",
      Email = "contact@techcorp.com",
      PhoneNumber = "+123456789",
      Address = "123 Tech Street, Silicon Valley, CA"
    }
  };
}
