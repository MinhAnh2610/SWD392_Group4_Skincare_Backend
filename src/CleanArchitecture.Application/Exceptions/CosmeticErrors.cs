using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exceptions
{
  public static class CosmeticErrors
  {
    public static readonly Error CosmeticNotFound = new Error("Cosmetic.NotFound", "There is no cosmetic with that id");
  }
}
