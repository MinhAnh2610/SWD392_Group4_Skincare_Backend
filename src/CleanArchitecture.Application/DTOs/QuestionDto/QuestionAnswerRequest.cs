namespace CleanArchitecture.Application.DTOs.QuestionDto;

public record QuestionAnswerRequest
  (
  Guid QuestionId,
  List<Guid> SelectedOptionIds // Supports multiple-choice questions
  );
