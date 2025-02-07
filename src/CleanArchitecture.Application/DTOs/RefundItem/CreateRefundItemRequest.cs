using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.RefundItem
{
    public class CreateRefundItemRequest
    {
        public int Quantity { get; set; }
        public string Reason { get; set; }
    }
}
