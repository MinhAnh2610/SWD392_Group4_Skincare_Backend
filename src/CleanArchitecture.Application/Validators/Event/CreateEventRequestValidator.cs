using CleanArchitecture.Application.DTOs.Events;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CleanArchitecture.Application.Validators.Event
{
  public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
  {
    public CreateEventRequestValidator()
    {
      RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
      RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
      RuleFor(x => x.DiscountPercentage)
        .GreaterThanOrEqualTo(0).WithMessage("Discount Percentage must be greater than or equal to 0")
        .LessThanOrEqualTo(100).WithMessage("Discount Percentage must be less than or equal to 100");
    }
  }
}