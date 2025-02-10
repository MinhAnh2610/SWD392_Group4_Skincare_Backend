using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;
  public ICompanyInformationRepository CompanyInformation { get; }
  public IPaymentRepository Payments { get; }
  public IRefundRepository Refunds { get; }
  public IRefundItemRepository RefundItems { get; }

  public UnitOfWork(ApplicationDbContext context)
  {
    _context = context;
    CompanyInformation = new CompanyInformationRepository(_context);
    Payments = new PaymentRepository(_context);
    Refunds = new RefundRepository(_context);
    RefundItems = new RefundItemRepository(_context);
  }

  public async Task<int> CompleteAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}
