using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Payment
{
    public class CreatePaymentRequest
    {
        public string? Reason { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? Method { get; set; }
    }
}
