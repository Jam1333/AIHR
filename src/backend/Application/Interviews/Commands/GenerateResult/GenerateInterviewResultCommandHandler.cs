using Application.Abstractions.AI;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.ValueObjects;
using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.GenerateResult;

internal sealed class GenerateInterviewResultCommandHandler(
    IInterviewRepository interviewRepository,
    IVacancyRepository vacancyRepository,
    ICandidateAnalyzer candidateAnalyzer,
    IRecruitmentAnalyzer recruitmentAnalyzer,
    IInterviewSummarizer interviewSummarizer) : ICommandHandler<GenerateInterviewResultCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(GenerateInterviewResultCommand command, CancellationToken cancellationToken)
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

        if (interview.HasResult)
        {
            return InterviewErrors.HasResult;
        }

        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(interview.VacancyId);

        if (vacancy is null)
        {
            return VacancyErrors.NotFound(interview.VacancyId);
        }

        Dictionary<string, string[]> interviewRequirements = await recruitmentAnalyzer.GetRequirementsAsync(
            interview.CandidateAnswersText, 
            interview.Language, 
            vacancy.Categories);
        Dictionary<string, Requirement[]> interviewRequirementsWithEmbeddings = await recruitmentAnalyzer.CreateRequirementsEmbeddingsAsync(interviewRequirements);

        RequirementsTotal requirementsTotal = candidateAnalyzer.CompareRequirements(vacancy.Requirements, interviewRequirementsWithEmbeddings, interview.Weights);
        string[] redFlags = await interviewSummarizer.GetCandidateRedFlags(interview.InterviewMessages, interview.Language);
        string[] inconsistencies = await interviewSummarizer.GetCandidateInconsistencies(interview.InterviewMessages, interview.ResumeText, interview.Language);

        var interviewResult = InterviewResult.Create(
            requirementsTotal, 
            redFlags, 
            inconsistencies);

        interview.ProvideInterviewResult(interviewResult);

        await interviewRepository.UpdateAsync(interview);

        return Unit.Value;
    }
}
