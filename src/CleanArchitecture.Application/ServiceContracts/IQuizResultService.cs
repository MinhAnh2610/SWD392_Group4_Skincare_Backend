using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.QuizResultDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IQuizResultService
{
  Task SaveQuizResultAsync(Guid customerId, Guid quizId, QuizSubmitRequest quizSubmission, RoutineResponse quizResponse);

  Task<Result<List<QuizResultResponse>>> GetUserQuizResultsAsync(Guid customerId);

  Task<Result<List<QuizResultResponse>>> GetAllCustomerQuizResultsAsync();
}
