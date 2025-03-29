using CleanArchitecture.Application.DTOs.BlogDto;
using IdentityModel;

namespace CleanArchitecture.Application.Validators.Blog
{
  public class CreateBlogRequestValidator : AbstractValidator<CreateBlogRequest>
  {
    public CreateBlogRequestValidator()
    {
      RuleFor(x => x.Title).Length(0, 512).WithMessage("Blog's title must be less than 512 characters");
      RuleFor(x => x.Title).NotEmpty().WithMessage("Blog's title is required");
      RuleFor(x => x.Content).NotEmpty().WithMessage("Blog's content is required");
    }
  }
}