namespace CleanArchitecture.Application.DTOs.QuestionOptionDto;

public record QuestionOptionUpdateRequest
  (
    Guid? Id,
    string Content,
    int Score
  );
