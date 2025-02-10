using Abp.Authorization.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRole = CleanArchitecture.Domain.Entities.UserRole;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(userRole => new {userRole.UserId, userRole.RoleId });
    }
}