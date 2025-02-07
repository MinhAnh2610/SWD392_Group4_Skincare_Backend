using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.RefundItem
{
    public class RefundItemResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
