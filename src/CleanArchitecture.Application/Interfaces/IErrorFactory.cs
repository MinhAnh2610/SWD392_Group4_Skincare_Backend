using FluentValidation.Results;

namespace CleanArchitecture.Application.Interfaces
{
  public interface IErrorFactory
  {
    (Error err, int statusCode) CreateNotFoundError(string objectName);
    (List<Error> errs, int statusCode) CreateValidationError(string objectName, ValidationResult validationResult);
    (Error err, int statusCode) CreateAlreadyExistsError(string objectName);
    (Error err, int statusCode) CreateDatabaseError(string objectName);
  }
}