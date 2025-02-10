namespace CleanArchitecture.Domain.Entities;

public class Role : IdentityRole<Guid>
{ 
    public List<UserRole>? UserRoles { get; set; } 
}
