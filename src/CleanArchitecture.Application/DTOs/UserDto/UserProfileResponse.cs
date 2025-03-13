using CleanArchitecture.Application.DTOs.SkinTypeDto;

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
  public SkinTypeResponse? SkinType { get; set; }
  public List<string>? Roles { get; set; } = new List<string>();
  public DateTime CreateAt { get; set; }
}
