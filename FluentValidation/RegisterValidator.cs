using BirthdayApp.DTO;
using FluentValidation;
using System.Data;

namespace BirthdayApp.FluentValidation
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email must not be empty")
              .EmailAddress().WithMessage("Incorrect Email Address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Your Password must have atleast 8 Characters");


            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter your Name")
                .MaximumLength(30).WithMessage("Name must not exceed 30 characters");
        }
    }
}
