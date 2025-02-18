using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IUserService
{
  Task<Result<UserProfileResponse>> GetUserProfile();
  Task<Result<UserProfileResponse>> UpdateUserProfileAsync(UpdateProfileRequest request);
  Task<Result<string>> EnableUserAsync(UserRequest request);
  Task<Result<string>> DisableUserAsync(UserRequest request);
}
