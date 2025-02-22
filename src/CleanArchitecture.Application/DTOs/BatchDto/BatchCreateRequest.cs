using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.BatchDto
{
  public class BatchCreateRequest
  {
    [Required]
    public Guid CosmeticId { get; set; }
    [Range(0, int.MaxValue)]
    [Required]
    public int Quantity { get; set; }
    [Required]
    public DateOnly ExportedDate { get; set; }
    [Required]
    public DateOnly ManufactureDate { get; set; }
    [Required]
    public DateOnly ExpirationDate { get; set; }
  }
}
