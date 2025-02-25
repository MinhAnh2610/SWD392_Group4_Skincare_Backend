using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.UserDto
{
  public class CustomerDto
  {
    public Guid? Id { get; set; } = default!;
    public string? UserName { get; set; } = default!;
    public string? Email { get; set; } = default!;
  }
}
