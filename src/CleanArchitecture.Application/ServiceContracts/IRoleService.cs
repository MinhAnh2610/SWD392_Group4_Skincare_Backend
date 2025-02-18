using CleanArchitecture.Application.DTOs.RoleDto;
using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IRoleService
{
  Task<Result<UserProfileResponse>> AssignRoleAsync(AssignRoleRequest request);
}
