using BirthdayApp.DTO;
using FluentValidation;

namespace BirthdayApp.FluentValidation
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must not be empty")
                .EmailAddress().WithMessage("Incorrect Email Address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Your Password must have atleast 8 Characters");
        }
    }
}
