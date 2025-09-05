using Application.Abstractions.FileSystem;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Events;
using Domain.ValueObjects;
using FluentResults;
using Mediator;

namespace Application.Analyses.Commands.Create;

internal sealed class CreateAnalysisCommandHandler(
    IAnalysisRepository analysisRepository,
    IVacancyRepository vacancyRepository,
    IFileReader fileReader,
    IEventBus eventBus) : ICommandHandler<CreateAnalysisCommand, Result<Guid>>
{
    public async ValueTask<Result<Guid>> Handle(CreateAnalysisCommand command, CancellationToken cancellationToken)
    {
        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(command.VacancyId);

        if (vacancy is null)
        {
            return VacancyErrors.NotFound(command.VacancyId);
        }

        if (vacancy.UserId != command.CurrentUserId)
        {
            return VacancyErrors.WrongAuthor;
        }

        FileContent[] fileContents = await fileReader.ReadAllTextsFromFilesRangeAsync(command.Files);

        var analysis = Analysis.Create(
            command.Title, 
            command.Weights, 
            vacancy);

        await analysisRepository.CreateAsync(analysis);

        await eventBus.PublishAsync(new AnalysisCreatedEvent(analysis.Id, fileContents));

        foreach (FileRequest file in command.Files)
        {
            file.Dispose();
        }

        return analysis.Id;
    }
}
