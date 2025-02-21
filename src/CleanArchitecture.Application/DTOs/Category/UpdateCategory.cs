using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Category
{
  public class UpdateCategory
  {
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
  }
}
