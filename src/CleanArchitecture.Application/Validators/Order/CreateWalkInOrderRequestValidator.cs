using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.DTOs.OrderDto;

namespace CleanArchitecture.Application.Validators.Order
{
  public class CreateWalkInOrderRequestValidator : AbstractValidator<CreateWalkInOrderRequest>
  {
    public CreateWalkInOrderRequestValidator()
    {
      RuleFor(request => request.Cosmetics)
        .NotEmpty()
        .WithMessage("Cosmetics cannot be empty");

      RuleFor(request => request.PaymentMethod)
        .NotEmpty()
        .WithMessage("Payment method cannot be empty")
        .Must(paymentMethod => PaymentMethods.ListPaymentMethods.Contains(paymentMethod.ToUpper()))
        .WithMessage("Payment method is invalid");
    }
  }
}