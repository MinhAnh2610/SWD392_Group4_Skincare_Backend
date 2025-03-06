namespace CleanArchitecture.Application.DTOs.UserDto;

public class UserProfileResponse
{
  public string? Id { get; set; } = default!;
  public string? UserName { get; set; } = default!;
  public string? Email { get; set; } = default!;
  public string? PhoneNumber { get; set; } = default!;
  public DateOnly? BirthDate { get; set; }
  public string? FirstName { get; set; } = default!;
  public string? LastName { get; set; } = default!;
  public bool Gender { get; set; }
  public string? SkinTypeId { get; set; }
  public string? SkinTypeName { get; set; }
  public List<string>? Roles { get; set; } = new List<string>();
}
