namespace CleanArchitecture.Application.DTOs.QuestionOptionDto;

public record QuestionOptionAddRequest
  (
    string Content,
    int Score
  );
