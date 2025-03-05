using FluentValidation;
using CleanArchitecture.Application.DTOs.CouponDTO;
using System;

namespace CleanArchitecture.Application.Validators.Coupon
{
  public class CreateCouponRequestValidator : AbstractValidator<CreateCouponRequest>
  {
    public CreateCouponRequestValidator()
    {
      RuleFor(x => x.Code)
          .NotEmpty().WithMessage("Coupon code is required.")
          .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.");

      RuleFor(x => x.Discount)
          .GreaterThan(0).WithMessage("Discount must be greater than zero.")
          .LessThanOrEqualTo(100).WithMessage("Discount cannot exceed 100%.");

      RuleFor(x => x.ExpiryDate)
          .GreaterThan(DateTime.UtcNow).WithMessage("Expiry date must be in the future.");

      RuleFor(x => x.UsageLimit)
          .GreaterThan(0).WithMessage("Usage limit must be greater than zero.");
    }
  }
}