namespace CleanArchitecture.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public DateOnly? BirthDate { get; set; } = new DateOnly();
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public bool Gender { get; set; } = true;
    public Guid? SkinTypeId { get; set; }
    public SkinType? SkinType { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public List<Blog> Blogs { get; set; } = new List<Blog>();
    public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public List<Refund> Refunds { get; set; } = new List<Refund>();
    public List<Order> Orders { get; set; } = new List<Order>();
}
