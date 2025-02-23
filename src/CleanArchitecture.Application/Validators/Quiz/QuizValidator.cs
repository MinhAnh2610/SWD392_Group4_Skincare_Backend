using CleanArchitecture.Application.DTOs.QuestionDto;
using CleanArchitecture.Application.DTOs.QuestionOptionDto;

namespace CleanArchitecture.Application.Validators.Quiz;

public class QuestionAddRequestValidator : AbstractValidator<QuestionAddRequest>
{
  public QuestionAddRequestValidator()
  {
    RuleFor(q => q.Title)
        .NotEmpty().WithMessage("Title is required.");

    RuleFor(q => q.Description)
        .NotEmpty().WithMessage("Description is required.");

    RuleFor(q => q.Instruction)
        .NotEmpty().WithMessage("Instruction is required.");

    RuleFor(q => q.Section)
        .NotEmpty().WithMessage("Section is required.");

    RuleFor(q => q.QuestionType)
        .NotEmpty().WithMessage("QuestionType is required.");

    RuleFor(q => q.QuestionOptions)
        .NotEmpty().WithMessage("At least one question option is required.")
        .Must(options => options.Count >= 2).WithMessage("At least two question options are required.");

    RuleForEach(q => q.QuestionOptions).SetValidator(new QuestionOptionAddRequestValidator());
  }
}

public class QuestionUpdateRequestValidator : AbstractValidator<QuestionUpdateRequest>
{
  public QuestionUpdateRequestValidator()
  {
    RuleFor(q => q.Title)
        .NotEmpty().WithMessage("Title is required.");

    RuleFor(q => q.Description)
        .NotEmpty().WithMessage("Description is required.");

    RuleFor(q => q.Instruction)
        .NotEmpty().WithMessage("Instruction is required.");

    RuleFor(q => q.Section)
        .NotEmpty().WithMessage("Section is required.");

    RuleFor(q => q.QuestionType)
        .NotEmpty().WithMessage("QuestionType is required.");

    RuleFor(q => q.QuestionOptions)
        .NotEmpty().WithMessage("At least one question option is required.")
        .Must(options => options.Count >= 2).WithMessage("At least two question options are required.");

    RuleForEach(q => q.QuestionOptions).SetValidator(new QuestionOptionUpdateRequestValidator());
  }
}

public class QuestionOptionAddRequestValidator : AbstractValidator<QuestionOptionAddRequest>
{
  public QuestionOptionAddRequestValidator()
  {
    RuleFor(opt => opt.Content)
        .NotEmpty().WithMessage("Content cannot be null or empty.");

    RuleFor(opt => opt.Score)
        .InclusiveBetween(-5, 5).WithMessage("Score must be between -5 and 5.");
  }
}

public class QuestionOptionUpdateRequestValidator : AbstractValidator<QuestionOptionUpdateRequest>
{
  public QuestionOptionUpdateRequestValidator()
  {
    RuleFor(opt => opt.Content)
        .NotEmpty().WithMessage("Content cannot be null or empty.");

    RuleFor(opt => opt.Score)
        .InclusiveBetween(-5, 5).WithMessage("Score must be between -5 and 5.");
  }
}
