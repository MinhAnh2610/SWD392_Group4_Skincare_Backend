using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Common;

public class Result<T>
{
  private Result(bool isSuccess, List<Error> errors, T? data, int status)
  {
    if (isSuccess && errors.Count > 0 ||
        !isSuccess && errors.Count == 0)
    {
      throw new ArgumentException("Invalid error", nameof(errors));
    }

    IsSuccess = isSuccess;
    Errors = errors;
    Data = data;
    Status = status;
  }

  public bool IsSuccess { get; }

  public bool IsFailure => !IsSuccess;

  public List<Error> Errors { get; }

  public T? Data { get; }

  public int Status { get; }

  public static Result<T> Success(T data, int status) => new Result<T>(true, new List<Error>(), data, status);

  public static Result<T> Failure(List<Error> errors, int status) => new Result<T>(false, errors, default, status);

  public IResult Match(string message)
  {
    var successResponse = ApiResponse<T>.SuccessResponse(Data, message);
    var failedResponse = ApiResponse<T>.FailureResponse(Errors, message);
    if (Errors.Count > 0)
    {
      message = string.Empty;
      foreach (var error in Errors)
      {
        message = message + $"{error.Description}\n";
      }
    }

    return Status switch
    {
      StatusCodes.Status200OK => Results.Ok(successResponse),
      StatusCodes.Status400BadRequest => Results.BadRequest(failedResponse),
      StatusCodes.Status401Unauthorized => Results.Unauthorized(),
      StatusCodes.Status409Conflict => Results.Conflict(failedResponse),
      StatusCodes.Status404NotFound => Results.NotFound(failedResponse),
      _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
    };
  }
}
