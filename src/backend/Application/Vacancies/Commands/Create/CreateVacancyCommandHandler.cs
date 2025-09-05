using Application.Abstractions.FileSystem;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Events;
using FluentResults;
using Mediator;

namespace Application.Vacancies.Commands.Create;

internal sealed class CreateVacancyCommandHandler(
    IVacancyRepository vacancyRepository,
    IUserRepository userRepository,
    IFileReader fileReader,
    IEventBus eventBus) : ICommandHandler<CreateVacancyCommand, Result<Guid>>
{
    public async ValueTask<Result<Guid>> Handle(CreateVacancyCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return UserErrors.NotFound(command.UserId);
        }

        string text = await fileReader.ReadAllTextAsync(command.File);

        command.File.Dispose();

        var vacancy = Vacancy.Create(
            command.Title,
            command.Language,
            text,
            user);

        await vacancyRepository.CreateAsync(vacancy);

        await eventBus.PublishAsync(new VacancyCreatedEvent(vacancy.Id, command.Categories));

        return vacancy.Id;
    }
}
