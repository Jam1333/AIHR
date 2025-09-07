using FluentValidation;

namespace Application.Interviews.Commands.ProvideContactInformation;

internal sealed class ProvideInterviewContactInformationCommandValidator : AbstractValidator<ProvideInterviewContactInformationCommand>
{
    public ProvideInterviewContactInformationCommandValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress();

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .MaximumLength(15);
    }
}
