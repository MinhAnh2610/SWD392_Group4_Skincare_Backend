using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.BatchDto
{
  public class BatchSearchRequest
  {
    public DateOnly ExportedDate { get; set; }
    public DateOnly ManufactureDate { get; set; }
    public DateOnly ExpirationDate { get; set; }
  }
}
