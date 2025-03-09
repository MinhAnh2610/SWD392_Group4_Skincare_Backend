using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.OrderDto;

namespace CleanArchitecture.Application.Validators.Order
{
  public class CreateWalkInOrderRequestValidator : AbstractValidator<CreateWalkInOrderRequest>
  {
    public CreateWalkInOrderRequestValidator()
    {
      // Rule for Cosmetics
      RuleFor(x => x.Cosmetics)
          .NotNull().WithMessage("Cosmetics cannot be null.")
          .NotEmpty().WithMessage("Cosmetics cannot be empty.")
          .Must(cosmetics => cosmetics != null && cosmetics.Count > 0)
          .WithMessage("At least one cosmetic item must be provided.")
          .Must(cosmetics => cosmetics != null && cosmetics.All(c => c.Value > 0))
          .WithMessage("Quantity for each cosmetic must be greater than 0.");

      // Rule for FirstName (optional)
      RuleFor(x => x.FirstName)
          .MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.");

      // Rule for LastName (optional)
      RuleFor(x => x.LastName)
          .MaximumLength(50).WithMessage("LastName cannot exceed 50 characters.");

      // Rule for CustomerPhoneNumber (optional but must be valid if provided)
      RuleFor(x => x.CustomerPhoneNumber)
          .NotEmpty().When(x => !string.IsNullOrEmpty(x.CustomerPhoneNumber))
          .WithMessage("CustomerPhoneNumber cannot be empty if provided.")
          .Matches(@"^\+?[0-9]{10,15}$")
          .When(x => !string.IsNullOrEmpty(x.CustomerPhoneNumber))
          .WithMessage("CustomerPhoneNumber must be a valid phone number.");

      // Rule for PaymentMethod (required)
      RuleFor(x => x.PaymentMethod)
          .NotNull().WithMessage("PaymentMethod cannot be null.")
          .NotEmpty().WithMessage("PaymentMethod cannot be empty.")
          .Must(paymentMethod => paymentMethod == "CASH" || paymentMethod == "ONLINE")
          .WithMessage("PaymentMethod must be either 'CASH' or 'ONLINE'.");
    }
  }
}