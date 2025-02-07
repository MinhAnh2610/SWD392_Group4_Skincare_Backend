using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Payment
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public string Reason { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }

}
