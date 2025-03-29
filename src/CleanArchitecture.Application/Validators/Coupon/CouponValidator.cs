using FluentValidation;
using CleanArchitecture.Application.DTOs.CouponDTO;

public class ApplyCouponRequestValidator : AbstractValidator<ApplyCouponRequest>
{
  public ApplyCouponRequestValidator()
  {
    RuleFor(x => x.code)
        .NotEmpty().WithMessage("Coupon code is required.")
        .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.");

    RuleFor(x => x.order)
        .NotEmpty().WithMessage("Order ID is required.")
        .NotEqual(Guid.Empty).WithMessage("Order ID must be a valid GUID.");

  }


}
