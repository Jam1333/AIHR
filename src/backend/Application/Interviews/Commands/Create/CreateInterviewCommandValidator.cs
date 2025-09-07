using FluentValidation;

namespace Application.Interviews.Commands.Create;

internal sealed class CreateInterviewCommandValidator : AbstractValidator<CreateInterviewCommand>
{
    public CreateInterviewCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.MaxMessagesCount)
            .GreaterThanOrEqualTo(3)
            .LessThanOrEqualTo(30);
    }
}
