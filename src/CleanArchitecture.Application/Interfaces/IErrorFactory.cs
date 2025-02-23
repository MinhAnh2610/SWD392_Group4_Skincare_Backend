namespace CleanArchitecture.Application.Interfaces
{
  public interface IErrorFactory
  {
    (Error err, int statusCode) CreateNotFoundError(string objectName);
    (Error err, int statusCode) CreateValidationError(string objectName);
    (Error err, int statusCode) CreateAlreadyExistsError(string objectName);
  }
}