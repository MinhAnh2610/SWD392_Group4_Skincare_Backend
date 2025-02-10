
namespace CleanArchitecture.Infrastructure.Repositories;

public class CompanyInformationRepository : GenericRepository<CompanyInformation>
{
  public CompanyInformationRepository(ApplicationDbContext context) : base(context)
  {
  }
}
