using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class UpdateCosmetic
  {
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string MainUsage { get; set; } = default!;
    [Required]
    public string Instructions { get; set; } = default!;
  }
}
