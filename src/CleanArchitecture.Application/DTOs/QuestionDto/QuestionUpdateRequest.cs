using CleanArchitecture.Application.DTOs.QuestionOptionDto;

namespace CleanArchitecture.Application.DTOs.QuestionDto;

public record QuestionUpdateRequest
  (
    string Title,
    string Description,
    string Instruction,
    string Section,
    string QuestionType,
    List<QuestionOptionUpdateRequest> QuestionOptions
  );
