using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;
   private readonly IPaymentRepository _paymentRepository;
    private readonly IRefundRepository _refundRepository;
    private readonly IRefundItemRepository _refundItemRepository;
    public UnitOfWork(ApplicationDbContext context)
     {
        _context = context;
        _paymentRepository = new PaymentRepository(_context);
        _refundRepository = new RefundRepository(_context);
        _refundItemRepository = new RefundItemRepository(_context);

    }

  public async Task<int> CompleteAsync()
  {
    return await _context.SaveChangesAsync();
  }
   public IPaymentRepository PaymentRepository => _paymentRepository;
    public IRefundRepository RefundRepository => _refundRepository;
    public IRefundItemRepository RefundItemRepository => _refundItemRepository;
    public void Dispose()
  {
    _context.Dispose();
  }
}
