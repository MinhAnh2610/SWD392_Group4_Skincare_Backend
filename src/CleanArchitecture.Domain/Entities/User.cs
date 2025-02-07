namespace CleanArchitecture.Domain.Entities;

public class User : IdentityUser<Guid>
{
  public DateOnly? BirthDate { get; set; } = new DateOnly();
  public string? FirstName { get; set; } = default!;
  public string? LastName { get; set; } = default!;
  public bool Gender { get; set; } = true;
  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenExpiration { get; set; }
}
