namespace CleanArchitecture.Application.Services
{
  public class RefundItemService : IRefundItemService
  {
    private readonly IUnitOfWork _unitOfWork;

    public RefundItemService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
  }
}
