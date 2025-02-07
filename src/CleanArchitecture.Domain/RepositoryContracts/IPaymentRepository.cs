// IPaymentRepository.cs

// IPaymentRepository.cs
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Domain.RepositoryContracts
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        public Task<List<Payment>> GetAllAsync()
        {
            // Your implementation here...
            throw new NotImplementedException();
        }
    }
}