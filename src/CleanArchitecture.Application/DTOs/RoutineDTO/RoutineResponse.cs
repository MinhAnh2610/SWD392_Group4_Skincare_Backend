using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.RoutineDTO
{
  public class RoutineResponse
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Period { get; set; } = default!;
    public Guid SkinTypeId { get; set; }
  }
}
