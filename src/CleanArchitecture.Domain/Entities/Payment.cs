using CleanArchitecture.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Payment : Entity<Guid>
    {
        public string TransactionId { get; set; } 
        public string Method { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

    }
}
