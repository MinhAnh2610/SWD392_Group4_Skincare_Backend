using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Exceptions
{
  public class ErrorFactory : IErrorFactory
  {
    public (Error err, int statusCode) CreateNotFoundError(string objectName)
    {
      return (new Error($"{objectName}.NotFound", $"{objectName} not found."), StatusCodes.Status404NotFound);
    }

    public (Error err, int statusCode) CreateValidationError(string objectName)
    {
      return (new Error($"{objectName}.Invalid", $"{objectName} is not valid."), StatusCodes.Status400BadRequest);
    }

    public (Error err, int statusCode) CreateAlreadyExistsError(string objectName)
    {
      return (new Error($"{objectName}.Duplicate", $"{objectName} already exists."), StatusCodes.Status409Conflict);
    }

    public (Error err, int statusCode) CreateInternalServerError(string objectName)
    {
      return (new Error($"{objectName}.ServerError", $"{objectName} causes errors in the server."), StatusCodes.Status500InternalServerError);
    }
  }
}