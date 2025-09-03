using Domain.Constants;
using FluentValidation;

namespace Application.Users.Commands.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .MinimumLength(3)
            .MaximumLength(30);

        RuleFor(u => u.Email)
            .EmailAddress();

        RuleFor(u => u.Password)
            .MinimumLength(8);
    }
}
