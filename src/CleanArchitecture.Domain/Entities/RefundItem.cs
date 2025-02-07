using CleanArchitecture.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class RefundItem : Entity<Guid>
    {
        public int Quantity { get; set; }
        public string Reason { get; set; }  
    }
}
