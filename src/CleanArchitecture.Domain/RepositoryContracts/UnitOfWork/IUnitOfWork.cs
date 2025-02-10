namespace CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
  ICompanyInformationRepository CompanyInformation { get; }
  IPaymentRepository Payments { get; }
  IRefundRepository Refunds { get; }
  IRefundItemRepository RefundItems { get; }

  Task<int> CompleteAsync();
}
