using CleanArchitecture.Application.DTOs.UserDto;

namespace CleanArchitecture.Application.Validators.User;

public class CreateWalkInUserValidator : AbstractValidator<CreateWalkInUserRequest>
{
  public CreateWalkInUserValidator()
  {
    RuleFor(x => x.UserName)
        .NotEmpty().WithMessage("Username is required.")
        .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

    RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

    RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required.")
        .MinimumLength(5).WithMessage("Password must be at least 5 characters long.");
  }
}
