using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
  public class TimeZoneService : ITimeZoneService
  {
    private readonly TimeZoneInfo _localTimeZone;

    public TimeZoneService()
    {
      _localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
    }

    public DateTime ConvertToLocalTime(DateTime utcTime)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(utcTime, _localTimeZone);
    }

    public DateTime ConvertToUtcTime(DateTime localTime)
    {
      return TimeZoneInfo.ConvertTimeToUtc(localTime, _localTimeZone);
    }
  }
}
