using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class FilterCosmeticRequest
  {
    public string? Name { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? TypeId { get; set; }
    public Guid? SkinTypeId { get; set; }
    
  }
}
