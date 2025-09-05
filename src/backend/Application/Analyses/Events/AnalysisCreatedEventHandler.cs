using Application.Abstractions.AI;
using Application.Abstractions.FileSystem;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Events;
using Domain.Extensions;
using Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Analyses.Events;

internal sealed class AnalysisCreatedEventHandler(
    IServiceScopeFactory serviceScopeFactory) : IEventHandler<AnalysisCreatedEvent>
{
    public async ValueTask Handle(AnalysisCreatedEvent analysisCreatedEvent, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var analysisRepository = scope.ServiceProvider.GetRequiredService<IAnalysisRepository>();
        var vacancyRepository = scope.ServiceProvider.GetRequiredService<IVacancyRepository>();

        var recruitmentAnalyser = scope.ServiceProvider.GetRequiredService<IRecruitmentAnalyzer>();
        var candidateAnalyser = scope.ServiceProvider.GetRequiredService<ICandidateAnalyzer>();

        var fileReader = scope.ServiceProvider.GetRequiredService<IFileReader>();

        var resumesRequirements = new Dictionary<string, Requirement[]>();

        Analysis? analysis = await analysisRepository.GetByIdAsync(analysisCreatedEvent.Id);

        if (analysis is null)
        {
            throw AnalysisErrors.NotFound(analysisCreatedEvent.Id).ToApplicationException();
        }

        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(analysis.VacancyId);

        if (vacancy is null)
        {
            throw VacancyErrors.NotFound(analysis.VacancyId).ToApplicationException();
        }

        string[] categories = vacancy.Categories;

        var resumeRequirementsEmbeddings = new Dictionary<string, IDictionary<string, Requirement[]>>();

        foreach (var fileContent in analysisCreatedEvent.Files)
        {
            var requirements = await recruitmentAnalyser.GetRequirementsAsync(fileContent.Text, vacancy.Language, categories);
            var requirementsEmbeddings = await recruitmentAnalyser.CreateRequirementsEmbeddingsAsync(requirements);

            resumeRequirementsEmbeddings[$"{fileContent.FileName}-{Guid.NewGuid()}"] = requirementsEmbeddings;
        }

        var requirementsTotals = candidateAnalyser.CompareRequirementsRange(vacancy.Requirements, resumeRequirementsEmbeddings, analysis.Weights);

        IEnumerable<ResumeResult> resumeResults = requirementsTotals.Select(rt => ResumeResult.Create(rt.Key, rt.Value));

        analysis.LoadResumeResults(resumeResults);

        await analysisRepository.UpdateAsync(analysis);
    }
}
