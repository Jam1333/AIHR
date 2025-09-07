using Application.Abstractions.AI;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.GenerateConclusion;

internal sealed class GenerateInterviewConclusionCommandHandler(
    IInterviewRepository interviewRepository,
    IInterviewSummarizer interviewSummarizer) : ICommandHandler<GenerateInterviewConclusionCommand, Result<GenerateInterviewConclusionResponse>>
{
    public async ValueTask<Result<GenerateInterviewConclusionResponse>> Handle(GenerateInterviewConclusionCommand command, CancellationToken cancellationToken)
    {
        Interview? interview = await interviewRepository.GetByIdAsync(command.Id);

        if (interview is null)
        {
            return InterviewErrors.NotFound(command.Id);
        }

        if (!interview.HasEnded)
        {
            return InterviewErrors.NotEnded;
        }

        if (interview.HasConclusion)
        {
            return InterviewErrors.HasConclusion;
        }

        string conclusion = await interviewSummarizer.GetInterviewConclusion(interview.InterviewMessages, interview.Language);

        interview.ProvideConclusion(conclusion);

        await interviewRepository.UpdateAsync(interview);

        return new GenerateInterviewConclusionResponse(conclusion);
    }
}
