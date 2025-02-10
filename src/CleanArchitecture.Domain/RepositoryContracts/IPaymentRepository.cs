// IPaymentRepository.cs

// IPaymentRepository.cs
using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IPaymentRepository : IGenericRepository<Payment>
{
}