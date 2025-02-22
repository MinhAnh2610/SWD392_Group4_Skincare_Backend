using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exceptions
{
  public class BatchErrors
  {
    public static readonly Error BatchNotFound = new Error("Batch.NotFound", "There is no batch with that id");
    public static readonly Error BatchAlreadyExist = new Error("Batch.AlreadyExist", "There is already a batch with that id");
  }
}
