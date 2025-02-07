namespace CleanArchitecture.Domain.Entities;

public class CompanyInformation : Entity<Guid>
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string LogoUrl { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string PhoneNumber { get; set; } = default!;
  public string Address { get; set; } = default!;
}
