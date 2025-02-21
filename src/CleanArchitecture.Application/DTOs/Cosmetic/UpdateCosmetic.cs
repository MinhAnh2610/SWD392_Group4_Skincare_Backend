using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class UpdateCosmetic
  {
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }
    public Guid SkinTypeId { get; set; }
    public Guid CosmeticTypeId { get; set; }
    public decimal Price { get; set; }
    public string MainUsage { get; set; } = default!;
    public string Instructions { get; set; } = default!;
  }
}
