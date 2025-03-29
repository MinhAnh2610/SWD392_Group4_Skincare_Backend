using CleanArchitecture.Domain.RepositoryContracts;

namespace CleanArchitecture.Infrastructure.Repositories;

public class CompanyInformationRepository : GenericRepository<CompanyInformation>, ICompanyInformationRepository
{
  public CompanyInformationRepository(ApplicationDbContext context) : base(context)
  {
  }
}
