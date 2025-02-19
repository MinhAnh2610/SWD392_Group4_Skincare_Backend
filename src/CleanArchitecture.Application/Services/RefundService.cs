namespace CleanArchitecture.Application.Services
{
  public class RefundService : IRefundService
  {
    private readonly IUnitOfWork _unitOfWork;
    public RefundService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
  }
}
