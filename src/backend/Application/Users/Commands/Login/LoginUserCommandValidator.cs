using FluentValidation;

namespace Application.Users.Commands.Login;

internal sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(u => u.Email)
            .EmailAddress();
    }
}
