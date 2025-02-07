using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Payment
{
    public class UpdatePaymentRequest
    {
        public Guid Id { get; set; }
        public string? Reason { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? Method { get; set; }
    }
}
