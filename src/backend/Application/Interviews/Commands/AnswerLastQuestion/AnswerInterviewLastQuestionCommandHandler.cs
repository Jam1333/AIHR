using Application.Abstractions.AI;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Events;
using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.AnswerLastQuestion;

internal sealed class AnswerInterviewLastQuestionCommandHandler(
    IInterviewRepository interviewRepository,
    IInterviewer interviewer,
    IEventBus eventBus) : ICommandHandler<AnswerInterviewLastQuestionCommand, Result<AnswerInterviewLastQuestionResponse>>
{
    public async ValueTask<Result<AnswerInterviewLastQuestionResponse>> Handle(AnswerInterviewLastQuestionCommand command, CancellationToken cancellationToken)
    {
        Interview? interview = await interviewRepository.GetByIdAsync(command.Id);

        if (interview is null)
        {
            return InterviewErrors.NotFound(command.Id);
        }

        if (interview.HasEnded)
        {
            return InterviewErrors.AlreadyEnded;
        }

        interview.AnswerLastQuestion(command.Answer);

        if (interview.HasEnded)
        {
            await interviewRepository.UpdateAsync(interview);

            await eventBus.PublishAsync(new InterviewEndedEvent(interview.Id, interview.UserId));

            return new AnswerInterviewLastQuestionResponse();
        }

        InterviewMessage nextQuestion = await interviewer.GenerateNextQuestionAsync(interview.InterviewMessages);

        interview.AddInterviewMessage(nextQuestion);

        await interviewRepository.UpdateAsync(interview);

        return new AnswerInterviewLastQuestionResponse(nextQuestion.Id);
    }
}
