using FluentValidation;

namespace Application.Users.Commands.Update;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .MinimumLength(3)
            .MaximumLength(30);

        RuleFor(u => u.Email)
            .EmailAddress();
    }
}
