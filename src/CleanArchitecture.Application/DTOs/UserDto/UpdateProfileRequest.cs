namespace CleanArchitecture.Application.DTOs.UserDto;

public record UpdateProfileRequest(
  string? UserName,
  string? PhoneNumber,
  DateOnly? BirthDate,
  string? FirstName,
  string? LastName,
  bool? Gender
  );
