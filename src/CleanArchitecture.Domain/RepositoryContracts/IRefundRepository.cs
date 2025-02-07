using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.RepositoryContracts
{
    public interface IRefundRepository : IGenericRepository<Refund>
    {
        Task<IEnumerable<Refund>> GetRefundsByStatus(string status);
        Task<IEnumerable<Refund>> GetRefundsByDateRange(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRefundAmount(string status);
    }
}
