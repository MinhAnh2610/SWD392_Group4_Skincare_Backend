using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface ITimeZoneService
  {
    DateTime ConvertToLocalTime(DateTime utcTime);
    DateTime ConvertToUtcTime(DateTime localTime);
  }
}
