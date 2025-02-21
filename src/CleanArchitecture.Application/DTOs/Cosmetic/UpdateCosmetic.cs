using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class UpdateCosmetic
  {
    public decimal Price { get; set; }
    public string MainUsage { get; set; } = default!;
    public string Instructions { get; set; } = default!;
  }
}
