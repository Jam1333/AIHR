using FluentValidation;

namespace Application.Interviews.Commands.AnswerLastQuestion;

internal sealed class AnswerInterviewLastQuestionCommandValidator : AbstractValidator<AnswerInterviewLastQuestionCommand>
{
    public AnswerInterviewLastQuestionCommandValidator()
    {
        RuleFor(c => c.Answer)
            .NotEmpty();
    }
}
