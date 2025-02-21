using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IQuizService
{
  Task<Result<QuizResponse>> GetQuizAsync();
  Task<Result<List<RoutineResponse>?>> ProcessQuizAsync(Guid id, QuizSubmitRequest request);
}
