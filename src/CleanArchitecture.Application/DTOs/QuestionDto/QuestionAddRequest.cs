using CleanArchitecture.Application.DTOs.QuestionOptionDto;

namespace CleanArchitecture.Application.DTOs.QuestionDto;

public record QuestionAddRequest 
  (
    string Title,
    string Description,
    string Instruction,
    string Section,
    string QuestionType,
    List<QuestionOptionAddRequest> QuestionOptions 
  );
