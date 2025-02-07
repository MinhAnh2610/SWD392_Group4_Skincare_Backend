using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.RefundItem
{
    public class UpdateRefundItemRequest
    {
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public string? Reason { get; set; }
    }
}
