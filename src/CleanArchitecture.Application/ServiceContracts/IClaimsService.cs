using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IClaimsService
  {
    public Guid CurrentUserId { get; }
    public List<string>? CurrentUserRoles { get; }
  }
}
