using CleanArchitecture.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Refund : Entity<Guid>
    {
        public string Reason { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Method { get; set; } 
    }
}
