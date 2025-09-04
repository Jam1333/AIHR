using Domain.Constants;
using FluentValidation;

namespace Application.Vacancies.Commands.Create;

internal sealed class CreateVacancyCommandValidator : AbstractValidator<CreateVacancyCommand>
{
    public CreateVacancyCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.Language)
            .Must(l => Languages.Available.Contains(l))
            .WithMessage("This language is not available");
    }
}
