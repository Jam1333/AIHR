using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Vacancies.Commands.Delete;

internal sealed class DeleteVacancyCommandHandler(
    IVacancyRepository vacancyRepository) : ICommandHandler<DeleteVacancyCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(DeleteVacancyCommand command, CancellationToken cancellationToken)
    {
        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(command.Id);

        if (vacancy is null)
        {
            return VacancyErrors.NotFound(command.Id);
        }

        await vacancyRepository.DeleteAsync(command.Id);

        return Unit.Value;
    }
}
