using Application.Abstractions.AI;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Events;
using Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Vacancies.Events;

internal sealed class VacancyCreatedEventHandler(
    IServiceScopeFactory serviceScopeFactory) : IEventHandler<VacancyCreatedEvent>
{
    public async ValueTask Handle(VacancyCreatedEvent vacancyCreatedEvent, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var vacancyRepository = scope.ServiceProvider.GetRequiredService<IVacancyRepository>();
        var vacancyAnalyzer = scope.ServiceProvider.GetRequiredService<IRecruitmentAnalyzer>();

        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(vacancyCreatedEvent.VacancyId);

        if (vacancy is null)
        {
            throw VacancyErrors.NotFound(vacancyCreatedEvent.VacancyId).ToApplicationException();
        }

        var requirements = await vacancyAnalyzer.GetRequirementsAsync(
            vacancy.Text, 
            vacancy.Language, 
            vacancyCreatedEvent.Categories);

        var requirementsEmbeddings = await vacancyAnalyzer.CreateRequirementsEmbeddingsAsync(requirements);

        vacancy.LoadRequirements(requirementsEmbeddings);
        await vacancyRepository.UpdateAsync(vacancy);
    }
}
