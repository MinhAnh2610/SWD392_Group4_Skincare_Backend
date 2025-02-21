using CleanArchitecture.Application.DTOs.QuestionDto;

namespace CleanArchitecture.Application.DTOs.QuizDto;

public record QuizSubmitRequest
  (
  List<QuestionAnswerRequest> Answers
  );
