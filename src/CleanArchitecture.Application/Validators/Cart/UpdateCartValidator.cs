using CleanArchitecture.Application.DTOs.CartDto;

namespace CleanArchitecture.Application.Validators.Cart;

public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
  public UpdateCartRequestValidator()
  {
    RuleFor(x => x.CartId)
        .NotEmpty().WithMessage("Cart ID is required.");

    RuleFor(x => x.Items)
        .NotNull().WithMessage("Cart items list cannot be null.")
        .Must(items => items.Any()).WithMessage("Cart must have at least one item.");

    RuleForEach(x => x.Items).SetValidator(new UpdateCartItemDtoValidator());
  }
}

public class UpdateCartItemDtoValidator : AbstractValidator<UpdateCartItemDto>
{
  public UpdateCartItemDtoValidator()
  {
    RuleFor(x => x.CosmeticId)
        .NotEmpty().WithMessage("Cosmetic ID is required.");

    RuleFor(x => x.Quantity)
        .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
  }
}
