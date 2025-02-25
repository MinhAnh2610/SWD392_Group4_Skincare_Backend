using CleanArchitecture.Application.DTOs.Cosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.BatchDto
{
  public class BatchResponse
  {
    public Guid Id { get; set; }
    public Guid CosmeticId { get; set; }
    public int Quantity { get; set; }
    public DateOnly ExportedDate { get; set; }
    public DateOnly ManufactureDate { get; set; }
    public DateOnly ExpirationDate { get; set; }
  }
}
