using CleanArchitecture.Application.DTOs.FeedbackDto;

namespace CleanArchitecture.Application.Validators.Feedback;

public class FeedbackValidator : AbstractValidator<FeedbackRequest>
{
  public FeedbackValidator()
  {
    RuleFor(f => f.CosmeticId)
        .NotEmpty().WithMessage("CosmeticId is required.");

    RuleFor(f => f.Rating)
        .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

    RuleFor(f => f.Content)
        .MaximumLength(200).WithMessage("Comment must be less than 200 characters.");
  }
}
