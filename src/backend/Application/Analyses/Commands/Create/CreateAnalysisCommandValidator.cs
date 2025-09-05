using FluentValidation;

namespace Application.Analyses.Commands.Create;

internal sealed class CreateAnalysisCommandValidator : AbstractValidator<CreateAnalysisCommand>
{
    public CreateAnalysisCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.Weights)
            .NotEmpty();

        RuleFor(c => c.Files)
            .NotEmpty();
    }
}
