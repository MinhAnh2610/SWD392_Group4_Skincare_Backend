namespace CleanArchitecture.Application.DTOs.RoleDto;

public record AssignRoleRequest(string? UserName, List<string>? Roles);
