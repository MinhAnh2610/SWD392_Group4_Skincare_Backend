using CleanArchitecture.Application.DTOs.QuestionDto;
using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IQuizService
{
  Task<Result<QuizResponse>> GetQuizAsync();
  Task<Result<List<RoutineResponse>?>> ProcessQuizAsync(Guid id, QuizSubmitRequest request);
  Task<Result<bool>> AddQuestionToQuizAsync(Guid quizId, QuestionAddRequest request);
  Task<Result<bool>> RemoveQuestionFromQuizAsync(Guid quizId, Guid questionId);
  Task<Result<bool>> UpdateQuestionAsync(Guid questionId, QuestionUpdateRequest request);
}
