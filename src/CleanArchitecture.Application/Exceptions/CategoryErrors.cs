using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exceptions
{
  public class CategoryErrors
  {
    public static readonly Error CategoryNotFound = new Error("Category.NotFound", "There is no category with that id");
    public static readonly Error CategoryAlreadyExist = new Error("Category.AlreadyExist", "There is already a category with that id");
    
  }
}
