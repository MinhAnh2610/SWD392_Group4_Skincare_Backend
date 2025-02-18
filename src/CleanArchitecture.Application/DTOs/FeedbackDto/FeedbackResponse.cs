using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.FeedbackDto
{
  public class FeedbackResponse
  {
    public Guid Id { get; set; }
    public Guid CosmeticId { get; set; }
    public Guid CustomerId { get; set; }
    public string? Content { get; set; }
    public decimal Rating { get; set; }
    public DateTime CreateAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
  }
}
