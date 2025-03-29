using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
  public UserRepository(ApplicationDbContext context) : base(context)
  {
    
  }

  public override async Task<List<User>> GetAllAsync()
  {
    return await _context.Users.Include(u => u.UserRoles).ToListAsync();
  }

  public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
  {
    return await _context.Users.Where(user => user.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
      
  }
}
