using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

       
    }
}